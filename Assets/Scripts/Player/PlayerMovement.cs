using Roguelike.Enemies;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using UnityEngine;

namespace Roguelike.Player
{
    [RequireComponent(typeof(PlayerAim))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float SmoothTime = 0.05f;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PlayerAim _playerAim;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private Vector3 _direction;
        private EnemyHealth _target;
        private float _currentVelocity;
        private bool _hasTarget;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _playerAim.TargetChanged += OnTargetChanged;

        private void OnDisable() => 
            _playerAim.TargetChanged -= OnTargetChanged;

        private void Update()
        {
            if (_playerHealth.IsAlive == false)
                return;
            
            _direction = GetDirection();
            _direction.Normalize();

            if (_hasTarget)
                RotateToTarget();
            else if (_direction.magnitude >= 0.1f)
                RotateToMoveDirection();
            
            _direction += Physics.gravity;
            Move();
        }

        private Vector3 GetDirection() =>
            new(_inputService.Axis.x, 0, _inputService.Axis.y);

        private void RotateToMoveDirection() => 
            Rotate(_direction);

        private void RotateToTarget()
        {
            Vector3 directionToTarget = _target.transform.position - transform.position;
            Rotate(directionToTarget.normalized);
        }

        private void Rotate(Vector3 direction)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            transform.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }

        private void Move()
        {
            _characterController.Move(_direction * _moveSpeed * Time.deltaTime);
            _playerAnimator.Move(_characterController.velocity.magnitude);
        }

        private void OnTargetChanged(EnemyHealth enemy)
        {
            _target = enemy;
            _hasTarget = (_target != null);
        }
    }
}