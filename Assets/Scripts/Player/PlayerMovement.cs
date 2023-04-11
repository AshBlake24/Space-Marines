using Roguelike.Infrastructure.Services;
using Roguelike.Services.Input;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float SmoothTime = 0.1f;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;
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
            
            if (_direction.magnitude >= 0.1f)
            {
                Move();
                Rotate();
            }
        }

        private Vector3 GetDirection() =>
            new(_inputService.Axis.x, 0, _inputService.Axis.y);

        private void Move() => 
            _characterController.Move(_direction * _moveSpeed * Time.deltaTime);

        private void Rotate()
        {
            float rotationAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            transform.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }
    }
}