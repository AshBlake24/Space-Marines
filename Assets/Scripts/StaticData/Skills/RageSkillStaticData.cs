using UnityEngine;

namespace Roguelike.StaticData.Skills
{
    [CreateAssetMenu(fileName = "Rage Skill", menuName = "Static Data/Skills/Rage")]
    public class RageSkillStaticData : SkillStaticData
    {
        [Header("Rage Stats")]
        [Range(0, 1f)] public float AttackSpeedMultiplier;
        [Range(1f, 30f)] public float SkillDuration;
        
        public override string GetLocalizedDescription() => 
            string.Format(Description.Value, AttackSpeedMultiplier * 100);
    }
}