using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    [CreateAssetMenu(fileName = "New Exploding Projectile", menuName = "Static Data/Projectiles/Exploding Projectile")]
    public class ExplodingProjectileStaticData : ProjectileStaticData
    {
        [Header("Exploding")]
        public float ExplodeRadius;
    }
}