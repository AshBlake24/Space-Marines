using Roguelike.Player.Skills;
using UnityEngine;

namespace Roguelike.StaticData.Skills
{
    public abstract class SkillStaticData : ScriptableObject
    {
        public SkillId Id;
        public Sprite Icon;
        [Range(5, 60)] public float SkillCooldown;

        [Header("VFX")] 
        public ParticleSystem SkillEffect;
    }
}