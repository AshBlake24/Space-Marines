using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Environment;
using Roguelike.Tutorials;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons.Tutorial
{
    public class ShowTutorialControls : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private ITutorialService _tutorialService;
        private IEnvironmentService _environmentService;

        private void OnEnable()
        {
            _environmentService = AllServices.Container.Single<IEnvironmentService>();
            
            if (_environmentService.GetDeviceType() == EnvironmentType.Desktop)
            {
                _tutorialService = AllServices.Container.Single<ITutorialService>();
                _button.onClick.AddListener(OnButtonClick);
            }
        }

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() => 
            _tutorialService.TryShowTutorial(TutorialId.Controls);
    }
}