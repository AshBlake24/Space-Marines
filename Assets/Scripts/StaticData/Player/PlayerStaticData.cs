using System.Collections.Generic;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        public CharacterStaticData StartCharacter;
        public float WeaponSwtichCooldown;
        public float ImmuneTimeAfterHit;
        public int MaxHealth;
    }
}