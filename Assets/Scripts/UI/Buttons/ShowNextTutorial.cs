using Roguelike.Infrastructure.Services;
using Roguelike.Tutorials;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    public class ShowNextTutorial : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TutorialId _nextTutorialId;
        
        private ITutorialService _tutorialService;

        private void OnEnable()
        {
            _tutorialService = AllServices.Container.Single<ITutorialService>();
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            _tutorialService.TryShowTutorial(_nextTutorialId);
        }
    }
}