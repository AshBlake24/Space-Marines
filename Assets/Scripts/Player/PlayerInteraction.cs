using System;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic.Interactables;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private const float DelayBeforeFindingTargets = 1f;

        [SerializeField] private LayerMask _interactablesLayerMask;
        [SerializeField] private float _updateTargetsPerFrame;
        [SerializeField, Range(0.5f, 5f)] private float _radius;
        [SerializeField] private List<MonoBehaviour> _componentsToDeactivateWhileInteraction;
        [SerializeField] private bool _drawGizmos;

        private readonly Collider[] _colliders = new Collider[3];
        private IInteractable _currentTargetInteractable;
        private IInputService _input;
        private IWindowService _windowService;
        private GameObject _weaponStatsViewer;
        private bool _isActive;

        public event Action GotInteractable;
        public event Action LostInteractable;

        private void OnDrawGizmos()
        {
            if (_drawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
        }

        public void Construct(IWindowService windowService, IInputService inputService)
        {
            _isActive = true;
            _windowService = windowService;
            _input = inputService;
            _input.Interacted += OnInteracted;
            _windowService.WindowOpened += OnWindowOpened;
            _windowService.WindowClosed += OnWindowClosed;
        }

        private void OnDestroy() =>
            _input.Interacted -= OnInteracted;

        private void Start()
        {
            InvokeRepeating(
                nameof(CheckForInteractables),
                DelayBeforeFindingTargets,
                (1 / _updateTargetsPerFrame));
        }

        public void Cleanup()
        {
            if (_currentTargetInteractable != null)
                _currentTargetInteractable.Outline.enabled = false;

            _currentTargetInteractable = null;
        }

        private void CheckForInteractables()
        {
            if (_isActive == false)
                return;

            int collidersInArea = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _colliders,
                _interactablesLayerMask);

            if (collidersInArea > 0)
            {
                IInteractable closestInteractable = FindClosestInteractable();

                if (InteractableIsCorrect(closestInteractable))
                {
                    ChangeCurrentInteractable(closestInteractable);
                    DisableComponents();
                }
                
                GotInteractable?.Invoke();
            }
            else
            {
                ClearCurrentInteractable();
                EnableComponents();
                LostInteractable?.Invoke();
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

        private void ChangeCurrentInteractable(IInteractable closestInteractable)
        {
            ClearCurrentInteractable();
            _currentTargetInteractable = closestInteractable;
            _currentTargetInteractable.Outline.enabled = true;

            if (_currentTargetInteractable is InteractableWeapon weapon)
                _weaponStatsViewer = _windowService.OpenWeaponStatsViewer(weapon.Id);
        }

        private void ClearCurrentInteractable()
        {
            if (_weaponStatsViewer != null)
                Destroy(_weaponStatsViewer);

            if (_currentTargetInteractable != null)
            {
                _currentTargetInteractable.Outline.enabled = false;
                _currentTargetInteractable = null;
            }
        }

        private void DisableComponents()
        {
            foreach (MonoBehaviour component in _componentsToDeactivateWhileInteraction)
                component.enabled = false;
        }

        private void EnableComponents()
        {
            foreach (MonoBehaviour component in _componentsToDeactivateWhileInteraction)
                component.enabled = true;
        }

        private void OnInteracted() =>
            _currentTargetInteractable?.Interact(gameObject);

        private bool InteractableIsCorrect(IInteractable closestInteractable)
        {
            if (InteractableExists(closestInteractable) == false)
                return false;

            if (InteractableIsDifferent(closestInteractable) == false)
                return false;

            if (InteractableIsActive(closestInteractable) == false)
                return false;

            return true;
        }

        private bool InteractableIsDifferent(IInteractable closestInteractable) =>
            closestInteractable != _currentTargetInteractable;

        private static bool InteractableExists(IInteractable closestInteractable) =>
            closestInteractable != null;

        private static bool InteractableIsActive(IInteractable closestInteractable) =>
            closestInteractable.IsActive;

        private void OnWindowClosed() => _isActive = true;

        private void OnWindowOpened()
        {
            _isActive = false;
            ClearCurrentInteractable();
        }
    }
}