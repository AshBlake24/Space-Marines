using Roguelike.Infrastructure.Services.Input;
using Roguelike.Player;

namespace Roguelike.UI.Buttons
{
    public class UseSkillButton : UIButton
    {
        private IInputService _inputService;
        private PlayerSkill _playerSkill;

        public void Construct(IInputService inputService, PlayerSkill playerSkill)
        {
            Button.interactable = true;
            _inputService = inputService;
            _playerSkill = playerSkill;
            
            _playerSkill.Skill.Performed += OnSkillPerformed;
            _playerSkill.SkillUsed += OnSkilUsed;
        }

        private void OnDestroy()
        {
            _playerSkill.Skill.Performed -= OnSkillPerformed;
            _playerSkill.SkillUsed -= OnSkilUsed;
        }

        private void OnSkilUsed() => 
            Button.interactable = false;

        private void OnSkillPerformed() => 
            Button.interactable = true;

        protected override void OnButtonClick() =>
            _inputService.UseSkill();
    }
}