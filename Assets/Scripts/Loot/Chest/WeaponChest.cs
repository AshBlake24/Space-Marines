using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Logic.Interactables;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class WeaponChest : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        
        private ILootFactory _lootFactory;

        public event Action Interacted;
        
        public Outline Outline => _outline;
        public bool IsActive { get; private set; }

        public void Construct(ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;
            IsActive = true;
        }

        // todo remove
        private void Awake()
        {
            _lootFactory = AllServices.Container.Single<ILootFactory>();
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            IsActive = false;
            Interacted?.Invoke();
            _lootFactory.CreateRandomWeapon(transform);
        }
    }
}