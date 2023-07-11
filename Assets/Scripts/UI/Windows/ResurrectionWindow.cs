using Roguelike.Ads;
using Roguelike.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class ResurrectionWindow : BaseWindow
    {
        private const float FadeValue = 0.25f;
        
        [SerializeField] private Button _resurrectButton;
        [SerializeField] private Image _usedLabel;

        private Image[] _resurrectButtonImages;
        private PlayerDeath _playerDeath;
        private IAdsService _adsService;

        public void Construct(PlayerDeath playerDeath, IAdsService adsService)
        {
            _playerDeath = playerDeath;
            _adsService = adsService;
        }

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
                _usedLabel.gameObject.SetActive(true);
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
                _usedLabel.gameObject.SetActive(false);
                _resurrectButton.interactable = true;
                _resurrectButton.onClick.AddListener(OnResurrectButtonClick);
            }
        }

        private void OnResurrectButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _adsService.ShowVideoAd(Resurrect);
#else
            Resurrect();
#endif
        }

        private void Resurrect()
        {
            ProgressService.PlayerProgress.State.HasResurrected = true;
            _playerDeath.Resurrect();
        }
    }
}