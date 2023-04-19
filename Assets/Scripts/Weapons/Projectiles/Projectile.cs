using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;
        
        public abstract void Construct<TStats>(TStats stats, IObjectPool<Projectile> projectilePool);
        public abstract void Init();
    }
}