using System;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
        
        public event Action<bool> WeaponChanged;

        public abstract bool IsAttackButtonUp();
        public void ChangeWeapon(bool switchToNext) => WeaponChanged?.Invoke(switchToNext);
    }
}