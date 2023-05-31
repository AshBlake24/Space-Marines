using System;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;
using Roguelike.Weapons.Stats;

namespace Roguelike.Weapons
{
    public interface IWeapon : IProgressWriter
    {
        WeaponStats Stats { get; }
        bool TryAttack();
        void Show();
        void Hide();
    }
}