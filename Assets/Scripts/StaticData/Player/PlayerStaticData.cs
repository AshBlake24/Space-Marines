using UnityEngine;

namespace Roguelike.StaticData.Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Static Data/Player/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        public float WeaponSwtichCooldown;
        public float ImmuneTimeAfterHit;
        public float ImmuneTimeAfterResurrect;
    }
}