using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public abstract class ProjectileStats
    {
        private readonly int _damage;
        private readonly float _speed;
        private readonly float _lifetime;

        protected ProjectileStats(ProjectileStaticData staticData)
        {
            _damage = staticData.Damage;
            _speed = staticData.Speed;
            _lifetime = staticData.Lifetime;
        }
        
        public int Damage => _damage;
        public float Speed => _speed;
        public float Lifetime => _lifetime;
    }
}