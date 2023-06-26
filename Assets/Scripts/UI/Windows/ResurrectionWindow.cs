using Agava.YandexGames;
using Roguelike.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class ResurrectionWindow : BaseWindow
    {
        private const float FadeValue = 0.25f;
        
        [SerializeField] private Button _resurrectButton;
        
        private Image[] _resurrectButtonImages;
        private PlayerDeath _playerDeath;

        public void Construct(PlayerDeath playerDeath) => 
            _playerDeath = playerDeath;

        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitResurrect();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            TimeService.ResumeGame();
            _resurrectButton.onClick.RemoveAllListeners();
        }

        private void InitResurrect()
        {
            _resurrectButtonImages = _resurrectButton.GetComponentsInChildren<Image>();
            
            if (ProgressService.PlayerProgress.State.HasResurrected)
            {
                _resurrectButton.interactable = false;

                foreach (Image image in _resurrectButtonImages)
                {
                    image.color = new Color(
                        image.color.r - FadeValue,
                        image.color.g - FadeValue,
                        image.color.b - FadeValue,
                        image.color.a);
                }
            }
            else
            {
                _resurrectButton.interactable = true;
                _resurrectButton.onClick.AddListener(OnResurrect);
            }
        }

        private void OnResurrect()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (YandexGamesSdk.IsInitialized)
                VideoAd.Show();
#endif
            ProgressService.PlayerProgress.State.HasResurrected = true;
            _playerDeath.Resurrect();
        }
    }
}