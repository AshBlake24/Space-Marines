using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public class ExplodingProjectileStats : ProjectileStats
    {
        private readonly float _explodingRadius;

        public ExplodingProjectileStats(ExplodingProjectileStaticData staticData) : base(staticData)
        {
            _explodingRadius = staticData.ExplodeRadius;
        }

        public float ExplodingRadius => _explodingRadius;
    }
}