using Roguelike.Data;
using Roguelike.Player.Enhancements;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon, IEnhanceable<int>
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _rotationOffset;

        public abstract WeaponStats Stats { get; }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        public abstract bool TryAttack();
        public abstract void Enhance(int extraDamageAtPercentage);
        public virtual void ReadProgress(PlayerProgress progress) { }
        public virtual void WriteProgress(PlayerProgress progress) { }
    }
}