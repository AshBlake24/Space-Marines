using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class InteractableWeapon : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _modelContainer;

        public WeaponId Id { get; private set; }
        public Outline Outline { get; private set; }
        public bool IsActive { get; private set; }
        public Transform ModelContainer => _modelContainer;

        public void Construct(WeaponId weaponId, Outline outline)
        {
            Id = weaponId;
            Outline = outline;
            Outline.enabled = false;
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.TryAddWeapon(Id, transform))
                {
                    IsActive = false;
                    interactor.GetComponent<PlayerInteraction>().Cleanup();
                    Destroy(gameObject);
                }
            }
        }
    }
}