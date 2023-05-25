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
        [SerializeField, Range(1f, 20f)] private float _radius;
        [SerializeField] private float _firePointHeight;
        [SerializeField] private bool _drawGizmos;

        private readonly Collider[] _colliders = new Collider[6];
        private EnemyHealth _closetEnemy;

        public event Action<EnemyHealth> TargetChanged;

        private void OnDrawGizmos()
        {
            if (_drawGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
        }

        private void Start()
        {
            InvokeRepeating(
                nameof(CheckTargets),
                DelayBeforeFindingTargets,
                (1 / _updateTargetsPerFrame));
        }

        private void CheckTargets()
        {
            _closetEnemy = null;

            int collidersInArea = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _colliders,
                _enemiesLayerMask);

            if (collidersInArea > 0)
                FindClosestTarget();

            TargetChanged?.Invoke(_closetEnemy);
        }

        private void FindClosestTarget()
        {
            float closestEnemyDistance = float.MaxValue;

            foreach (Collider collider in _colliders)
            {
                if (collider == null)
                    continue;

                if (collider.gameObject.TryGetComponent(out EnemyHealth enemyHealth)
                    && LineOfFireIsFree(enemyHealth.transform))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemyHealth.transform.position);

                    if (distanceToEnemy < closestEnemyDistance)
                    {
                        closestEnemyDistance = distanceToEnemy;
                        _closetEnemy = enemyHealth;
                    }
                }
            }
        }

        private bool LineOfFireIsFree(Transform enemyTransform)
        {
            Vector3 raycastOrigin = transform.position;
            raycastOrigin.y = _firePointHeight;
            Vector3 direction = enemyTransform.position - raycastOrigin;
            Physics.Raycast(raycastOrigin, direction, out RaycastHit hit, _radius);

            return (1 << hit.transform.gameObject.layer) == _enemiesLayerMask.value;
        }
    }
}