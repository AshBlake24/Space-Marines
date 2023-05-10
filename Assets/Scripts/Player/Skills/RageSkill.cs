using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player.Skills
{
    public class RageSkill : Skill
    {
        private readonly PlayerShooter _playerShooter;
        private readonly float _attackSpeedMultiplier;
        private readonly float _skillDuration;

        public RageSkill(
            ICoroutineRunner coroutineRunner,
            PlayerShooter playerShooter,
            float attackSpeedMultiplier,
            float skillDuration,
            float skillCooldown,
            ParticleSystem skillEffect) : base(coroutineRunner, skillCooldown, skillEffect)
        {
            _playerShooter = playerShooter;
            _attackSpeedMultiplier = attackSpeedMultiplier;
            _skillDuration = skillDuration;
        }

        public override bool Boosted => _playerShooter.Boosted;
        
        public override event Action Performed;

        public override void UseSkill() => 
            CoroutineRunner.StartCoroutine(Rage());

        private IEnumerator Rage()
        {
            IsActive = true;
            ReadyToUse = false;

            _playerShooter.SetAttackSpeedMultiplier(_attackSpeedMultiplier);
            
            yield return Helpers.GetTime(_skillDuration);

            _playerShooter.ResetAttackSpeedMultiplier();

            IsActive = false;
            Performed?.Invoke();
            CoroutineRunner.StartCoroutine(SkillCooldown());
        }
    }
}