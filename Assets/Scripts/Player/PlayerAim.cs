using System;
using Roguelike.Enemies;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private const float DelayBeforeFindingTargets = 1f;

        [SerializeField] private LayerMask _enemiesLayerMask;
        [SerializeField] private float _updateTargetsPerFrame;
        [SerializeField] private float _radius;

        private readonly Collider[] _colliders = new Collider[6];
        private EnemyHealth _target;

        public event Action<EnemyHealth> TargetFound;

        private void Start()
        {
            InvokeRepeating(
                nameof(CheckTargets), 
                DelayBeforeFindingTargets, 
                (1 / _updateTargetsPerFrame));
        }

        private void CheckTargets()
        {
            Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _enemiesLayerMask);

            EnemyHealth closestEnemy = null;
            float closestEnemyDistance = float.MaxValue;

            foreach (Collider collider in _colliders)
            {
                if (collider == null)
                    continue;
                
                if (collider.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemyHealth.transform.position);

                    if (distanceToEnemy < closestEnemyDistance)
                    {
                        closestEnemyDistance = distanceToEnemy;
                        closestEnemy = enemyHealth;
                    }
                }
            }

            if (closestEnemy != null && closestEnemy != _target)
                TargetFound?.Invoke(closestEnemy);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}