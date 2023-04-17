using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    [CreateAssetMenu(fileName = "New Shrapnel", menuName = "Static Data/Projectiles/Shrapnel")]
    public class ShrapnelStaticData : ProjectileStaticData
    {
        public int BulletsPerShot;
    }
}