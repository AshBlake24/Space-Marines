using UnityEngine;
using Roguelike.Weapons.Stats;

namespace Roguelike.Weapons
{
    public interface IWeapon
    {
        WeaponStats Stats { get; }
        Vector3 PositionOffset { get; }
        Vector3 RotationOffset { get; }
        bool TryAttack();
        void Show();
        void Hide();
    }
}