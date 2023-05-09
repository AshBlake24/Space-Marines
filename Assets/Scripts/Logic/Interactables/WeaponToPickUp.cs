using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    [RequireComponent(typeof(Outline), typeof(Collider))]
    public class WeaponToPickUp : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponId _weaponId;
        [SerializeField] private Outline _outline;

        public WeaponId Id => _weaponId;
        public Outline Outline => _outline;

        private void Start() =>
            _outline.enabled = false;

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.TryAddWeapon(_weaponId, transform))
                {
                    interactor.GetComponent<PlayerInteraction>().Cleanup();
                    Destroy(gameObject);
                }
            }
        }
    }
}