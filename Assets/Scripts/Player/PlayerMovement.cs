using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float SmoothTime = 0.1f;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private Vector3 _direction;
        private float _currentVelocity;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Update()
        {
            _direction = GetDirection();
            _direction.Normalize();

            if (_direction.magnitude >= 0.1f)
                Rotate();
            
            _direction += Physics.gravity;
            Move();
        }

        private Vector3 GetDirection() =>
            new(_inputService.Axis.x, 0, _inputService.Axis.y);

        private void Move()
        {
            _characterController.Move(_direction * _moveSpeed * Time.deltaTime);
            _playerAnimator.Move(_characterController.velocity.magnitude);
        }

        private void Rotate()
        {
            float rotationAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            transform.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }
    }
}