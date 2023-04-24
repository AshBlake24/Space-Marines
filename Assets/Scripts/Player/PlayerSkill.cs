using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Player.Skills;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerSkill : MonoBehaviour
    {
        private IInputService _input;
        private ISkill _skill;

        private void Awake() =>
            _input = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _input.SkillUsed += OnUseSkill;

        private void OnDisable() => 
            _input.SkillUsed -= OnUseSkill;

        public void Construct(ISkill skill) =>
            _skill = skill;

        private void OnUseSkill()
        {
            if (_skill.ReadyToUse && _skill.IsActive == false)
                _skill.UseSkill();
        }
    }
}