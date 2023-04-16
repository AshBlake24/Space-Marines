using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public class DesktopInputService : InputService
    {
        private readonly PlayerInput _playerInput;
        
        public DesktopInputService()
        {
            _playerInput = new PlayerInput();
            InitializePlayerInput();
        }

        public override Vector2 Axis =>
            _playerInput.Player.Move.ReadValue<Vector2>();

        private void InitializePlayerInput()
        {
            _playerInput.Enable();
            _playerInput.Player.Attack.performed += (ctx) => OnAttack();
            _playerInput.Player.SwitchWeapon.performed += (ctx) =>
            {
                float value = ctx.ReadValue<float>();
                OnWeaponChanged(value > 0);
            };
        }
    }
}