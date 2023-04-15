using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public abstract void Attack();
    }
}