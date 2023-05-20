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

        // TODO: replace to construct method
        private void Awake()
        {
            _lootFactory = AllServices.Container.Single<ILootFactory>();
            Outline.enabled = false;
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            IsActive = false;
            Interacted?.Invoke();
            GameObject weapon = _lootFactory.CreateRandomWeapon(transform.position);
            weapon.transform.rotation = transform.rotation;
        }
    }
}