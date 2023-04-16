using System;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HorizontalAxis = "Horizontal";
        protected const string VerticalAxis = "Vertical";

        public abstract Vector2 Axis { get; }
        public event Action Attack;
        public event Action<bool> WeaponChanged;

        public virtual void OnAttack() => Attack?.Invoke();
        public virtual void OnWeaponChanged(bool switchToNext) => WeaponChanged?.Invoke(switchToNext);
    }
}