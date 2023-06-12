using System;
using Roguelike.Infrastructure.AssetManagement;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }

        public event Action<bool> WeaponChanged;
        public event Action SkillUsed;
        public event Action Interacted;
        public event Action PausePressed;

        public abstract bool IsAttackButtonUp();
        public void ChangeWeapon(bool switchToNext) => WeaponChanged?.Invoke(switchToNext);
        public void UseSkill() => SkillUsed?.Invoke();
        public void Interact() => Interacted?.Invoke();
        protected void Pause() => PausePressed?.Invoke();
    }
}