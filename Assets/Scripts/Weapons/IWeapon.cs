using System;
using UnityEngine;
using Roguelike.Weapons.Stats;

namespace Roguelike.Weapons
{
    public interface IWeapon
    {
        WeaponStats Stats { get; }
        bool TryAttack();
        void Show();
        void Hide();
    }
}