using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Weapons.PickupableWeapons
{
    [CreateAssetMenu(fileName = "PickupableWeaponsStaticData", menuName = "Static Data/Weapons/Pickupable Weapon")]
    public class PickupableWeaponsStaticData : ScriptableObject
    {
        public List<PickupableWeaponsConfig> Configs;
    }
}