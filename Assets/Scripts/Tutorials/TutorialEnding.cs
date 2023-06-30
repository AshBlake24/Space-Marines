using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialEnding : MonoBehaviour
    {
        private ITutorialService _tutorialService;
        private IPersistentDataService _persistentData;

        public void Construct(IPersistentDataService persistentData, ITutorialService tutorialService)
        {
            _persistentData = persistentData;
            _tutorialService = tutorialService;
        }

        private void Start()
        {
            if (_persistentData.PlayerProgress.TutorialData.IsTutorialCompleted)
            {
                Destroy(gameObject);
                return;
            }

            LoadingScreen.Hided += OnLoadingScreenHided;
        }

        private void OnLoadingScreenHided()
        {
            LoadingScreen.Hided -= OnLoadingScreenHided;

            _tutorialService.TryShowTutorial(TutorialId.Ending01);
            
            Destroy(gameObject);
        }
    }
}