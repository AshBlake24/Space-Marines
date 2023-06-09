using Roguelike.Audio;
using Roguelike.Audio.Logic;
using Roguelike.Data;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;
        
        protected int TotalDamage;

        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _rotationOffset;
        public abstract WeaponStats Stats { get; }
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        public abstract bool TryAttack();
        public virtual void Enhance(int extraDamageAtPercentage)
        {
            if (extraDamageAtPercentage > 0)
            {
                int additiveDamage = Stats.Damage * extraDamageAtPercentage / 100;
                TotalDamage = Stats.Damage + additiveDamage;
            }
        }
        public virtual void ReadProgress(PlayerProgress progress) { }
        public virtual void WriteProgress(PlayerProgress progress) { }
    }
}