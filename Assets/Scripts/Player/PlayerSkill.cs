using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Player.Skills;
using Unity.VisualScripting;
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
            if (SkillIsReady())
            {
                _skill.UseSkill();
                _skillEffect.Play();
                SkillUsed?.Invoke();
            }
        }

        private bool SkillIsReady()
        {
            if (_skill.ReadyToUse == false)
                return false;

            if (_skill.IsActive)
                return false;

            if (_skill.Boosted)
                return false;

            return true;
        }

        private void OnSkillPerformed() => 
            _skillEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
    }
}