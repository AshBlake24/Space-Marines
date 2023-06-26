using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.StaticData.Skills;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public abstract class Skill : ISkill
    {
        protected readonly ICoroutineRunner CoroutineRunner;

        protected Skill(
            ICoroutineRunner coroutineRunner,
            float skillCooldown,
            ParticleSystem skillEffect)
        {
            CoroutineRunner = coroutineRunner;
            Cooldown = skillCooldown;
            VFX = skillEffect;
            ReadyToUse = true;
            IsActive = false;
        }

        public abstract event Action Performed;
        
        public abstract SkillId Id { get; }
        public ParticleSystem VFX { get; }
        public float Cooldown { get; }
        public bool IsActive { get; protected set; }
        public bool ReadyToUse { get; protected set; }
        public virtual bool Boosted { get; }

        public abstract void UseSkill();
        
        protected IEnumerator SkillCooldown()
        {
            yield return Helpers.GetTime(Cooldown);

            ReadyToUse = true;
        }
    }
}