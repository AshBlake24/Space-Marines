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
        private ParticleSystem _skillEffect;
        
        public event Action SkillUsed;
        
        public ISkill Skill => _skill;

        private void Awake() =>
            _input = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _input.SkillUsed += OnUseSkill;

        private void OnDisable() => 
            _input.SkillUsed -= OnUseSkill;

        private void OnDestroy() => 
            _skill.Performed -= OnSkillPerformed;

        public void Construct(ISkill skill)
        {
            _skill = skill;
            _skillEffect = Instantiate(skill.VFX, transform);
            _skill.Performed += OnSkillPerformed;
            OnSkillPerformed();

        }

        private void OnUseSkill()
        {
            if (_skill.ReadyToUse && _skill.IsActive == false)
            {
                _skill.UseSkill();
                _skillEffect.Play();
                SkillUsed?.Invoke();
            }
        }

        private void OnSkillPerformed() => 
            _skillEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
    }
}