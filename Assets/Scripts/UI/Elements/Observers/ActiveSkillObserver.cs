using JetBrains.Annotations;
using Roguelike.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Observers
{
    public class ActiveSkillObserver : MonoBehaviour
    {
        [SerializeField] private Image _cooldownMask;
        [SerializeField, CanBeNull] private Image _icon;

        private PlayerSkill _playerSkill;
        private float _elapsedTime;
        private float _cooldownDurationNormalized;
        private bool _cooldown;

        public void Construct(PlayerSkill playerSkill, Sprite skillIcon)
        {
            if (_icon != null) 
                _icon.sprite = skillIcon;

            _playerSkill = playerSkill;
            _cooldownMask.fillAmount = 0f;
            _cooldown = false;

            _playerSkill.Skill.Performed += OnSkillPerformed;
            _playerSkill.SkillUsed += OnSkilUsed;
        }

        private void OnDestroy()
        {
            _playerSkill.Skill.Performed -= OnSkillPerformed;
            _playerSkill.SkillUsed -= OnSkilUsed;
        }

        private void Update()
        {
            if (_cooldown)
            {
                _cooldownMask.fillAmount = Mathf.InverseLerp(_playerSkill.Skill.Cooldown, 0f, _elapsedTime);
                _elapsedTime += Time.deltaTime;

                if (_cooldownMask.fillAmount == 0f)
                    _cooldown = false;
            }
        }

        private void OnSkillPerformed()
        {
            _elapsedTime = 0f;
            _cooldown = true;
        }

        private void OnSkilUsed() => 
            _cooldownMask.fillAmount = 1f;
    }
}