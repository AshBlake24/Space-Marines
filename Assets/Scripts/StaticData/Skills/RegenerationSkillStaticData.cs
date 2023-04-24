using Roguelike.Player.Skills;
using UnityEngine;

namespace Roguelike.StaticData.Skills
{
    [CreateAssetMenu(fileName = "Regeneration Skill", menuName = "Static Data/Skills/Regeneration")]
    public class RegenerationSkillStaticData : SkillStaticData
    {
        public const int HealthPerTick = 1;
        
        [Header("Regeneration Stats")]
        [Range(1, 3)] public int TicksCount;
        [Range(1f, 5f)] public float CooldownBetweenTicks;
    }
}