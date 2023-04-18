using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class Shrapnel : Projectile
    {
        private ShrapnelStats _stats;

        public override void Construct<TStats>(TStats stats, ObjectPool<Projectile> pool)
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}