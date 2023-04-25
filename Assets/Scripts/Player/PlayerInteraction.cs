using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private const float DelayBeforeFindingTargets = 1f;
        
        [SerializeField] private LayerMask _interactablesLayerMask;
        [SerializeField] private float _updateTargetsPerFrame;
        [SerializeField, Range(0.5f, 5f)] private float _radius;
        [SerializeField] private bool _drawGizmos;

        private readonly Collider[] _colliders = new Collider[3];
        private IInteractable _closest;
        private IInputService _input;

        private void OnDrawGizmos()
        {
            if (_drawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
        }
        
        private void Awake() => 
            _input = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _input.Interacted += OnInteracted;

        private void OnDisable() => 
            _input.Interacted -= OnInteracted;

        private void Start()
        {
            InvokeRepeating(
                nameof(CheckForInteractables),
                DelayBeforeFindingTargets,
                (1 / _updateTargetsPerFrame));
        }

        private void CheckForInteractables()
        {
            _closest = null;

            int collidersInArea = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _colliders,
                _interactablesLayerMask);

            if (collidersInArea > 0)
                FindClosest();
        }

        private void FindClosest()
        {
            float closestObjectDistance = float.MaxValue;

            foreach (Collider collider in _colliders)
            {
                if (collider == null)
                    continue;

                if (collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    float distanceToObject = Vector3.Distance(transform.position, collider.transform.position);

                    if (distanceToObject < closestObjectDistance)
                    {
                        closestObjectDistance = distanceToObject;
                        _closest = interactable;
                    }
                }
            }
        }

        private void OnInteracted() => 
            _closest?.Interact();
    }
}