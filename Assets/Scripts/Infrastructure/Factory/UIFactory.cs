using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Ads;
using Roguelike.Animations.UI;
using Roguelike.Audio.Service;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic.Pause;
using Roguelike.Loot.Chest;
using Roguelike.Player;
using Roguelike.StaticData.Enhancements;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Windows;
using Roguelike.UI.Buttons;
using Roguelike.UI.Elements;
using Roguelike.UI.Elements.Audio;
using Roguelike.UI.Elements.Views;
using Roguelike.UI.Windows;
using Roguelike.UI.Windows.Confirmations;
using Roguelike.UI.Windows.Enhancements;
using Roguelike.UI.Windows.Panels;
using Roguelike.UI.Windows.Regions;
using Roguelike.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentDataService _progressService;
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly IRandomService _randomService;
        private readonly ITimeService _timeService;
        private readonly IAudioService _audioService;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IAdsService _adsService;

        private Transform _uiRoot;
        private Transform _tutorialRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IPersistentDataService progressService, ISceneLoadingService sceneLoadingService,
            IRandomService randomService, ITimeService timeService, IAudioService audioService,
            IWeaponFactory weaponFactory, IAdsService adsService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
            _sceneLoadingService = sceneLoadingService;
            _randomService = randomService;
            _timeService = timeService;
            _audioService = audioService;
            _weaponFactory = weaponFactory;
            _adsService = adsService;
        }

        public BaseWindow CreateWindow<TKey>(IWindowService windowService, TKey windowId, bool isTutorial) 
            where TKey : Enum
        {
            WindowConfig<TKey> config = _staticData.GetDataById<TKey, WindowConfig<TKey>>(windowId);
            BaseWindow window = Object.Instantiate(config.WindowPrefab, isTutorial ? _uiRoot : _tutorialRoot);
            window.Construct(_progressService, _timeService);

            foreach (OpenWindowButton openWindowButton in window.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(windowService);

            switch (window)
            {
                case MainMenu mainMenu:
                    mainMenu.Construct(_staticData, _sceneLoadingService, windowService);

                    break;
                case ConfirmationWindow confirmationWindow:
                    confirmationWindow.Construct(_staticData, _sceneLoadingService);

                    break;
                case GameCompleteWindow gameOverWindow:
                    InitGameOverWindow(gameOverWindow);

                    break;
                case RegionSelectionWindow regionSelectionWindow:
                    regionSelectionWindow.Construct(_staticData);

                    break;
                case OptionsMenu optionsMenu:
                    InitOptionsMenu(optionsMenu);

                    break;
                case PauseMenu pauseMenu:
                    pauseMenu.Construct(this);

                    break;
                case StatisticsWindow statisticsWindow:
                    InitStatisticsWindow(statisticsWindow);
                    
                    break;
                case CharacterStats characterStats:
                    characterStats.Construct(_staticData, _weaponFactory);

                    break;
            }

            return window;
        }

        public GameObject CreateWeaponStatsViewer(IWindowService windowService, WeaponId weaponId)
        {
            GameObject weaponStatsViewer = _assetProvider.Instantiate(AssetPath.WeaponStatsViewer, _uiRoot);
            WeaponStaticData weaponData = _staticData.GetDataById<WeaponId, WeaponStaticData>(weaponId);

            if (weaponData is RangedWeaponStaticData rangedWeaponData)
            {
                if (weaponStatsViewer.TryGetComponent(out RangedWeaponStatsViewer rangedWeaponStatsViewer))
                    rangedWeaponStatsViewer.Construct(_progressService, rangedWeaponData);
            }

            return weaponStatsViewer;
        }

        public EnhancementShopWindow CreateEnhancementShop(IWindowService windowService, PlayerEnhancements playerEnhancements)
        {
            BaseWindow window = CreateWindow(windowService, WindowId.EnhancementShop, isTutorial: false);

            if (window is EnhancementShopWindow shop)
            {
                shop.Construct(_staticData, _progressService, _randomService, playerEnhancements);
                return shop;
            }

            throw new ArgumentNullException(nameof(window));
        }

        public void CreateResurrectionWindow(IWindowService windowService, PlayerDeath playerDeath)
        {
            BaseWindow window = CreateWindow(windowService, WindowId.Resurrection, isTutorial: false);

            if (window is ResurrectionWindow resurrectionWindow)
                resurrectionWindow.Construct(playerDeath);
        }

        public void CreateEnhancementWidget(EnhancementData enhancementProgress, Transform parent, 
            EnhancementTooltip tooltip)
        {
            GameObject gameObject = _assetProvider.Instantiate(AssetPath.EnhancementWidgetPath, parent);

            if (gameObject.TryGetComponent(out PlayerEnhancementWidget widget))
            {
                EnhancementStaticData enhancementData = _staticData
                    .GetDataById<EnhancementId, EnhancementStaticData>(enhancementProgress.Id);
                
                widget.Construct(tooltip, enhancementData, enhancementProgress);
            }
            else
            {
                throw new ArgumentNullException(nameof(widget));
            }
        }

        public void CreateWeaponChestWindow(WindowService windowService, SalableWeaponChest salableWeaponChest)
        {
            BaseWindow window = CreateWindow(windowService, WindowId.WeaponChestWindow, isTutorial: false);

            if (window is WeaponChestWindow weaponChestWindow)
                weaponChestWindow.Construct(_adsService, salableWeaponChest);
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
        
        public void CreateTutorialRoot() =>
            _tutorialRoot = _assetProvider.Instantiate(AssetPath.TutorialRootPath).transform;

        public void ShowStageName()
        {
            string stageLabel = _progressService.PlayerProgress.WorldData.CurrentStage.ToLabel();

            _assetProvider.Instantiate(AssetPath.StageNameViewerPath, _uiRoot)
                .GetComponent<StageNameViewer>()
                .Show(stageLabel);
        }

        private void InitGameOverWindow(GameCompleteWindow gameCompleteWindow)
        {
            gameCompleteWindow.Construct(_staticData, _sceneLoadingService);
            
            if (gameCompleteWindow.TryGetComponent(out GameCompleteWindowAnimations gameOverWindowAnimations))
            {
                GameObject[] weapons = 
                    CreateWeaponViews(gameOverWindowAnimations.WeaponsContent);
                
                GameObject[] enhancements = 
                    CreateEnhancementViews(gameOverWindowAnimations.EnhancementsContent);
                
                gameOverWindowAnimations.Construct(weapons, enhancements);
            }
        }

        private GameObject[] CreateEnhancementViews(Transform parent)
        {
            List<GameObject> enhancements = new();
            List<EnhancementData> playerEnhancements = _progressService.PlayerProgress.State.Enhancements;

            foreach (EnhancementData enhancementData in playerEnhancements)
            {
                EnhancementStaticData staticData = _staticData
                    .GetDataById<EnhancementId, EnhancementStaticData>(enhancementData.Id);

                GameObject instance = _assetProvider.Instantiate(AssetPath.EnhancementViewPath, parent);

                if (instance.TryGetComponent(out EnhancementView view) == false)
                {
                    throw new ArgumentNullException(nameof(instance), 
                        $"Instance does not contain {nameof(EnhancementView)} component");
                }
                
                view.Construct(staticData.Icon, enhancementData.Tier);
                instance.transform.localScale = Vector2.zero;
                enhancements.Add(instance);
            }

            return enhancements.ToArray();
        }

        private GameObject[] CreateWeaponViews(Transform parent)
        {
            List<GameObject> weapons = new();
            WeaponId[] availableWeapons = _progressService.PlayerProgress.PlayerWeapons.Weapons;

            foreach (WeaponId weaponId in availableWeapons)
            {
                if (weaponId == WeaponId.Unknown)
                    continue;
                
                WeaponStaticData staticData = _staticData
                    .GetDataById<WeaponId, WeaponStaticData>(weaponId);
                
                Dictionary<RarityId, Color> rarityColors = _staticData.GetAllDataByType<RarityId, RarityStaticData>()
                    .ToDictionary(data => data.Id, data => data.Color);
                
                GameObject instance = _assetProvider.Instantiate(AssetPath.WeaponViewPath, parent);
                
                if (instance.TryGetComponent(out WeaponView view) == false)
                {
                    throw new ArgumentNullException(nameof(instance), 
                        $"Instance does not contain {nameof(WeaponView)} component");
                }
                
                view.Construct(staticData.FullsizeIcon, rarityColors[staticData.Rarity]);
                instance.transform.localScale = Vector2.zero;
                weapons.Add(instance);
            }

            return weapons.ToArray();
        }

        private void InitOptionsMenu(OptionsMenu optionsMenu)
        {
            optionsMenu.GetComponentInChildren<AudioSettingsWindow>()
                .Construct(_audioService);
            
            optionsMenu.GetComponentInChildren<LanguageSelector>()
                .Construct(_progressService.PlayerProgress.Settings);
        }
        
        private void InitStatisticsWindow(StatisticsWindow statisticsWindow)
        {
            StatisticsPanel statisticsPanel = statisticsWindow.GetComponentInChildren<StatisticsPanel>();
            statisticsPanel.Construct(_progressService, _staticData);
            statisticsWindow.Construct(statisticsPanel);
        }
    }
}