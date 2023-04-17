using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public abstract class ProjectileStats
    {
        private readonly int _damage;
        private readonly float _speed;

        protected ProjectileStats(ProjectileStaticData staticData)
        {
            _damage = staticData.Damage;
            _speed = staticData.Speed;
        }
        
        public int Damage => _damage;
        public float Speed => _speed;
    }
}