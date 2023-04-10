using System;
using Roguelike.Infrastructure;
using Roguelike.Services.Input;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 direction = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                direction.x = _inputService.Axis.x;
                direction.y = 0f;
                direction.z = _inputService.Axis.y;
                direction.Normalize();

                transform.forward = direction;
            }
            _characterController.Move(direction * _moveSpeed * Time.deltaTime);
        }
    }
}