using System;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        event Action<bool> WeaponChanged;
        bool IsAttackButtonUp();
        void ChangeWeapon(bool switchToNext);
    }
}