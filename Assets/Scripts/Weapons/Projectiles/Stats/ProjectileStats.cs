using Roguelike.StaticData.Projectiles;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public abstract class ProjectileStats
    {
        private readonly int _damage;
        private readonly float _speed;
        private readonly float _lifetime;
        private readonly ParticleSystem _muzzleFlashVFX;
        private readonly ParticleSystem _projectileVFX;
        private readonly ParticleSystem _impactVFX;

        protected ProjectileStats(ProjectileStaticData staticData)
        {
            _damage = staticData.Damage;
            _speed = staticData.Speed;
            _lifetime = staticData.Lifetime;
            _muzzleFlashVFX = staticData.MuzzleFlashVFX;
            _projectileVFX = staticData.ProjectileVFX;
            _impactVFX = staticData.ImpactVFX;
        }
        
        public int Damage => _damage;
        public float Speed => _speed;
        public float Lifetime => _lifetime;
        public ParticleSystem MuzzleFlashVFX => _muzzleFlashVFX;
        public ParticleSystem ProjectileVFX => _projectileVFX;
        public ParticleSystem ImpactVFX => _impactVFX;
    }
}