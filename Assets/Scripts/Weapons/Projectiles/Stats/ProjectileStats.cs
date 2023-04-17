using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public abstract class ProjectileStats
    {
        private readonly ProjectileId _id;
        private readonly int _damage;
        private readonly float _speed;

        protected ProjectileStats(ProjectileStaticData staticData)
        {
            _id = staticData.Id;
            _damage = staticData.Damage;
            _speed = staticData.Speed;
        }

        public ProjectileId ID => _id;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}