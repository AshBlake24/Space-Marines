using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Logic.Interactables;
using UnityEngine;

namespace Roguelike.Loot
{
    public class WeaponChest : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        
        private ILootFactory _lootFactory;

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
            _lootFactory.CreateWeapon(transform.position + transform.forward);
            IsActive = false;
        }
    }
}