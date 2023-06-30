using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Tutorials
{
    public class EndPlayerTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialWindow _window;
        
        private IPersistentDataService _persistentData;

        private void Awake()
        {
            _persistentData = AllServices.Container.Single<IPersistentDataService>();
            _window.Closed += OnClosed;
        }
        private void OnDestroy() => _window.Closed -= OnClosed;

        private void OnClosed(BaseWindow window) => 
            _persistentData.PlayerProgress.TutorialData.IsTutorialCompleted = true;
    }
}