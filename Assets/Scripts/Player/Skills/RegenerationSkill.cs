using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Logic;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public class RegenerationSkill : ISkill
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IHealth _targetHealth;
        private readonly int _healthPerTick;
        private readonly int _ticksCount;
        private readonly float _cooldownBetweenTicks;
        private readonly float _cooldown;
        private readonly ParticleSystem _skillEffect;

        public event Action Performed;

        public RegenerationSkill(
            ICoroutineRunner coroutineRunner, 
            IHealth targetHealth, 
            int healthPerTick, 
            int ticksCount,
            float cooldownBetweenTicks,
            float skillCooldown,
            ParticleSystem skillEffect)
        {
            _coroutineRunner = coroutineRunner;
            _targetHealth = targetHealth;
            _healthPerTick = healthPerTick;
            _ticksCount = ticksCount;
            _cooldownBetweenTicks = cooldownBetweenTicks;
            _cooldown = skillCooldown;
            _skillEffect = skillEffect;
            ReadyToUse = true;
            IsActive = false;
        }

        public ParticleSystem VFX => _skillEffect;
        public bool IsActive { get; private set; }
        public bool ReadyToUse { get; private set; }
        
        public void UseSkill() => 
            _coroutineRunner.StartCoroutine(Healing());

        private IEnumerator SkillCooldown()
        {
            yield return Helpers.GetTime(_cooldown);

            ReadyToUse = true;
        }

        private IEnumerator Healing()
        {
            IsActive = true;
            ReadyToUse = false;

            for (int i = 0; i < _ticksCount; i++)
            {
                _targetHealth.Heal(_healthPerTick);

                yield return Helpers.GetTime(_cooldownBetweenTicks);
            }
            
            Performed?.Invoke();
            _coroutineRunner.StartCoroutine(SkillCooldown());
        }
    }
}