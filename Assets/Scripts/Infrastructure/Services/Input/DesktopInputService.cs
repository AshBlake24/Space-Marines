using UnityEngine;
using UnityEngine.InputSystem;

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
            _playerInput.Player.UseSkill.performed += (ctx) => OnSkillUsed();
            _playerInput.Player.SwitchWeapon.performed += (ctx) =>
            {
                float value = ctx.ReadValue<float>();
                ChangeWeapon(value > 0);
            };
        }

        private void OnSkillUsed() => UseSkill();

        public override bool IsAttackButtonUp() =>
            _playerInput.Player.Attack.phase == InputActionPhase.Performed;
    }
}