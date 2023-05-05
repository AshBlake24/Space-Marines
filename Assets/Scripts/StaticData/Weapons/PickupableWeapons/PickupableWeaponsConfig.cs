using System;
using UnityEngine;

namespace Roguelike.StaticData.Weapons.PickupableWeapons
{
    [Serializable]
    public class PickupableWeaponsConfig
    {
        public WeaponId Id;
        public GameObject Prefab;
    }
}