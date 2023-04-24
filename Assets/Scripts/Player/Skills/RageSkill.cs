using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public class RageSkill : ISkill
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly PlayerShooter _playerShooter;
        private readonly float _attackSpeedMultiplier;
        private readonly float _skillDuration;
        private readonly float _cooldown;

        public event Action Performed;

        public RageSkill(
            ICoroutineRunner coroutineRunner,
            PlayerShooter playerShooter,
            float attackSpeedMultiplier,
            float skillDuration,
            float skillCooldown,
            ParticleSystem skillEffect)
        {
            _coroutineRunner = coroutineRunner;
            _playerShooter = playerShooter;
            _attackSpeedMultiplier = attackSpeedMultiplier;
            _skillDuration = skillDuration;
            _cooldown = skillCooldown;
            VFX = skillEffect;
            ReadyToUse = true;
            IsActive = false;
        }

        public ParticleSystem VFX { get; }
        public bool IsActive { get; private set; }
        public bool ReadyToUse { get; private set; }
        
        public void UseSkill() => 
            _coroutineRunner.StartCoroutine(Rage());

        private IEnumerator SkillCooldown()
        {
            yield return Helpers.GetTime(_cooldown);

            ReadyToUse = true;
        }

        private IEnumerator Rage()
        {
            IsActive = true;
            ReadyToUse = false;

            _playerShooter.SetAttackSpeedMultiplier(_attackSpeedMultiplier);
            
            yield return Helpers.GetTime(_skillDuration);

            _playerShooter.ResetAttackSpeedMultiplier();

            IsActive = false;
            Performed?.Invoke();
            _coroutineRunner.StartCoroutine(SkillCooldown());
        }
    }
}