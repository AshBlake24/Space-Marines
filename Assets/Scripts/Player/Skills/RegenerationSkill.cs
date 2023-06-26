using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Logic;
using Roguelike.StaticData.Skills;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public class RegenerationSkill : Skill
    {
        private readonly IHealth _targetHealth;
        private readonly int _healthPerTick;
        private readonly int _ticksCount;
        private readonly float _cooldownBetweenTicks;

        public RegenerationSkill(
            ICoroutineRunner coroutineRunner,
            IHealth targetHealth, 
            int healthPerTick, 
            int ticksCount,
            float cooldownBetweenTicks,
            float skillCooldown,
            ParticleSystem skillEffect) : base(coroutineRunner, skillCooldown, skillEffect)
        {
            _targetHealth = targetHealth;
            _healthPerTick = healthPerTick;
            _ticksCount = ticksCount;
            _cooldownBetweenTicks = cooldownBetweenTicks;
        }

        public override SkillId Id => SkillId.Regeneration;

        public override event Action Performed;

        public override void UseSkill() => 
            CoroutineRunner.StartCoroutine(Healing());

        private IEnumerator Healing()
        {
            IsActive = true;
            ReadyToUse = false;

            for (int i = 0; i < _ticksCount; i++)
            {
                _targetHealth.Heal(_healthPerTick);

                yield return Helpers.GetTime(_cooldownBetweenTicks);
            }

            IsActive = false;
            Performed?.Invoke();
            CoroutineRunner.StartCoroutine(SkillCooldown());
        }
    }
}