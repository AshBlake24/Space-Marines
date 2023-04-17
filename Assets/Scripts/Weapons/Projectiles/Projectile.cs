using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public abstract ProjectileStats Stats { get; }
    }
}