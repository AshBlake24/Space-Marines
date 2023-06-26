using System;
using Roguelike.StaticData.Skills;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public interface ISkill
    {
        event Action Performed;
        SkillId Id { get; }
        ParticleSystem VFX { get; }
        float Cooldown { get; }
        bool IsActive { get; }
        bool ReadyToUse { get; }
        bool Boosted { get; }
        void UseSkill();
    }
}