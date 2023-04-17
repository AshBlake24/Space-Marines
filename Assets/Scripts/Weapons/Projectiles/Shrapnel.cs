using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class Shrapnel : Projectile
    {
        private ShrapnelStats _stats;
        
        public override ProjectileStats Stats => _stats;

        public void Construct(ShrapnelStats stats)
        {
            _stats = stats;
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}