using Roguelike.Ads;
using Roguelike.Localization;
using Roguelike.Loot.Chest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class WeaponChestWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private Button _openForCoinsButton;
        [SerializeField] private Button _openForAdsButton;
        [SerializeField] private Image _coinsButtonFadeColorImage;
        [SerializeField] private int _openCost;

        private IAdsService _adsService;
        private SalableWeaponChest _chest;

        public void Construct(IAdsService adsService, SalableWeaponChest weaponChest)
        {
            _adsService = adsService;
            _chest = weaponChest;
        }

        protected override void Initialize()
        {
            if (ProgressService.PlayerProgress.TutorialData.IsTutorialCompleted == false)
            {
                _priceLabel.text = LocalizedConstants.Free.Value;
                _openForCoinsButton.interactable = true;
                _openForAdsButton.interactable = false;
                _coinsButtonFadeColorImage.gameObject.SetActive(false);
            }
            else
            {
                _priceLabel.text = _openCost.ToString();
                _openForCoinsButton.interactable = PlayerHasMoney();
                _openForAdsButton.interactable = true;
                _coinsButtonFadeColorImage.gameObject.SetActive(true);
            }
        }

        protected override void SubscribeUpdates()
        {
            _openForCoinsButton.onClick.AddListener(OnOpenForCoins);
            _openForAdsButton.onClick.AddListener(OnOpenForAds);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _openForCoinsButton.onClick.RemoveListener(OnOpenForCoins);
            _openForAdsButton.onClick.RemoveListener(OnOpenForAds);
        }

        private void OnOpenForCoins()
        {
            ProgressService.PlayerProgress.Balance.HubBalance.WithdrawCoins(_openCost);
            OpenChest();
        }

        private void OnOpenForAds()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _adsService.ShowVideoAd(OpenChest);
#else
            OpenChest();
#endif
        }

        private void OpenChest() => _chest.Open();

        private bool PlayerHasMoney() => 
            ProgressService.PlayerProgress.Balance.HubBalance.Coins >= _openCost;
    }
}