using System;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
        public event Action<bool> Attacking;
        public event Action<bool> WeaponChanged;

        protected void OnAttacking(bool isAtacking) => Attacking?.Invoke(isAtacking);
        protected void OnWeaponChanged(bool switchToNext) => WeaponChanged?.Invoke(switchToNext);
    }
}