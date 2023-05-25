using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Skills
{
    public abstract class SkillStaticData : ScriptableObject, IStaticData
    {
        public SkillId Id;
        public Sprite Icon;
        public string Description;
        [Range(5, 60)] public float SkillCooldown;

        [Header("VFX")] 
        public ParticleSystem SkillEffect;
    }
}