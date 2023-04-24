using System;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public interface ISkill
    {
        event Action Performed;
        ParticleSystem VFX { get; }
        bool IsActive { get; }
        bool ReadyToUse { get; }
        void UseSkill();
    }
}