using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public abstract class ProjectileStats
    {
        private readonly ProjectileId _id;
        private readonly int _damage;
        private readonly float _speed;
        private readonly float _spread;

        protected ProjectileStats(ProjectileStaticData staticData)
        {
            _id = staticData.Id;
            _damage = staticData.Damage;
            _speed = staticData.Speed;
            _spread = staticData.Spread;
        }
    }
}