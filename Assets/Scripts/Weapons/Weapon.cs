using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _rotationOffset;
        
        public abstract WeaponStats Stats { get; }
        public abstract void Attack();
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}