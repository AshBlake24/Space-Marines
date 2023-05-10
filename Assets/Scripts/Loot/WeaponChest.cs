using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Logic.Interactables;
using UnityEngine;

namespace Roguelike.Loot
{
    public class WeaponChest : MonoBehaviour, IInteractable
    {
        private static readonly int s_open = Animator.StringToHash("Open");
        
        [SerializeField] private Outline _outline;
        [SerializeField] private Animator _animator;
        
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
            PlayOpen();
            IsActive = false;
        }

        private void PlayOpen() => 
            _animator.SetTrigger(s_open);

        private void OnOpened() => 
            _lootFactory.CreateRandomWeapon(transform.position + transform.forward);
    }
}