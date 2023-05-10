using System;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class InteractableWeapon : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _modelContainer;

        public WeaponId Id { get; private set; }
        public Outline Outline { get; private set; }
        public Transform ModelContainer => _modelContainer;

        public void Construct(WeaponId weaponId, Outline outline)
        {
            Id = weaponId;
            Outline = outline;
            Outline.enabled = false;
        }

        private void Update() => 
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.TryAddWeapon(Id, transform))
                {
                    interactor.GetComponent<PlayerInteraction>().Cleanup();
                    Destroy(gameObject);
                }
            }
        }
    }
}