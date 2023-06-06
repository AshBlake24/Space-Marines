using Roguelike.Enemies;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Player.Enhancements;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Player
{
    [RequireComponent(typeof(PlayerAim))]
    public class PlayerMovement : MonoBehaviour, IEnhanceable<int>
    {
        private const float SmoothTime = 0.05f;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private PlayerAim _playerAim;
        [SerializeField] private Transform _playerAimTarget;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private Vector3 _direction;
        private EnemyHealth _target;
        private float _currentVelocity;
        private float _defaultMoveSpeed;
        private float _baseMoveSpeed;
        private bool _hasTarget;
        
        public bool Boosted { get; private set; }

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _baseMoveSpeed = _moveSpeed;
            Boosted = false;
        }

        private void OnEnable() => 
            _playerAim.TargetChanged += OnTargetChanged;

        private void OnDisable() => 
            _playerAim.TargetChanged -= OnTargetChanged;

        private void Update()
        {
            _direction = GetDirection();
            _direction.Normalize();

            if (_hasTarget && _target != null)
                RotateToTarget();
            else if (_direction.magnitude >= 0.01f)
                RotateToMoveDirection();
            
            _direction += Physics.gravity;
            Move();
        }

        public void BoostSpeed(float speedMultiplier)
        {
            _moveSpeed *= speedMultiplier;
            Boosted = true;
        }

        public void ResetSpeed()
        {
            _moveSpeed = _defaultMoveSpeed;
            Boosted = false;
        }

        public void Enhance(int moveSpeedPercentage)
        {
            float additionalMoveSpeed = _baseMoveSpeed * moveSpeedPercentage / 100;
            _defaultMoveSpeed = _baseMoveSpeed + additionalMoveSpeed;
            _moveSpeed = _defaultMoveSpeed;
        }

        private Vector3 GetDirection() =>
            new(_inputService.Axis.x, 0, _inputService.Axis.y);

        private void RotateToMoveDirection() => 
            Rotate(_direction);

        private void RotateToTarget()
        {
            _playerAimTarget.position = _target.transform.position;
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
            _playerAnimator.Move(_characterController.velocity.normalized);
        }

        private void OnTargetChanged(EnemyHealth enemy)
        {
            _target = enemy;
            _hasTarget = (_target != null);
            _playerAnimator.Aim(_hasTarget);
        }
    }
}