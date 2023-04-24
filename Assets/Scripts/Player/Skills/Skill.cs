using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public abstract class Skill : ISkill
    {
        protected readonly ICoroutineRunner CoroutineRunner;
        private readonly float _cooldown;

        protected Skill(
            ICoroutineRunner coroutineRunner,
            float skillCooldown,
            ParticleSystem skillEffect)
        {
            CoroutineRunner = coroutineRunner;
            _cooldown = skillCooldown;
            VFX = skillEffect;
            ReadyToUse = true;
            IsActive = false;
        }
        
        public abstract event Action Performed;
        
        public ParticleSystem VFX { get; }
        public bool IsActive { get; protected set; }
        public bool ReadyToUse { get; protected set; }

        public abstract void UseSkill();
        
        protected IEnumerator SkillCooldown()
        {
            yield return Helpers.GetTime(_cooldown);

            ReadyToUse = true;
        }
    }
}