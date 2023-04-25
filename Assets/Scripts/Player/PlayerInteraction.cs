using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private const float DelayBeforeFindingTargets = 1f;
        private const float OutlineWidth = 5f;
        
        [SerializeField] private LayerMask _interactablesLayerMask;
        [SerializeField] private float _updateTargetsPerFrame;
        [SerializeField, Range(0.5f, 5f)] private float _radius;
        [SerializeField] private bool _drawGizmos;

        private readonly Collider[] _colliders = new Collider[3];
        private IInteractable _currentTargetInteractable;
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
            int collidersInArea = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _colliders,
                _interactablesLayerMask);

            if (collidersInArea > 0)
            {
                IInteractable closestInteractable = FindClosestInteractable();

                if (closestInteractable != null && closestInteractable != _currentTargetInteractable)
                {
                    ClearOutline();
                    _currentTargetInteractable = closestInteractable;
                    RenderOutline();
                }
            }
            else
            {
                ClearOutline();
                _currentTargetInteractable = null;
            }
        }

        private IInteractable FindClosestInteractable()
        {
            IInteractable closestInteractable = null;
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
                        closestInteractable = interactable;
                    }
                }
            }

            return closestInteractable;
        }

        private void RenderOutline()
        {
            if (_currentTargetInteractable.gameObject.TryGetComponent(out Outline outline))
            {
                outline.enabled = true;
            }
            else
            {
                Outline outlineComponent = _currentTargetInteractable.gameObject.AddComponent<Outline>();
                outlineComponent.OutlineMode = Outline.Mode.OutlineAll;
                outlineComponent.OutlineColor = Color.white;
                outlineComponent.OutlineWidth = OutlineWidth;
            }
        }

        private void ClearOutline()
        {
            if (_currentTargetInteractable == null)
                return;;
            
            if (_currentTargetInteractable.gameObject.TryGetComponent(out Outline outline))
                outline.enabled = false;
        }

        private void OnInteracted() => 
            _currentTargetInteractable?.Interact();
    }
}