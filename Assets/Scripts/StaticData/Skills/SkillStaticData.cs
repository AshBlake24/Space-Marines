using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using UnityEngine;

namespace Roguelike.StaticData.Skills
{
    public abstract class SkillStaticData : ScriptableObject, IStaticData
    {
        public SkillId Id;
        public Sprite Icon;
        public LocalizedString Name;
        public LocalizedString Description;
        [Range(5, 60)] public float SkillCooldown;

        [Header("VFX")] 
        public ParticleSystem SkillEffect;
        
        public Enum Key => Id;
    }
}