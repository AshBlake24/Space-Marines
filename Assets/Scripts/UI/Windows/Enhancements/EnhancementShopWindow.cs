using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.StaticData.Enhancements;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Windows.Enhancements
{
    public class EnhancementShopWindow : BaseWindow
    {
        [SerializeField] private Transform _content;
        [SerializeField] private EnhancementViewer _enhancementViewerPrefab;
        [SerializeField] private TextMeshProUGUI _playerBalance;
        [SerializeField, Range(1, 3)] private int _enhancementsCount;

        private IStaticDataService _staticDataService;
        private IPersistentDataService _progressService;
        private IRandomService _random;
        private PlayerEnhancements _playerEnhancements;

        public void Construct(IStaticDataService staticDataService, IPersistentDataService persistentData, 
            IRandomService randomService, PlayerEnhancements playerEnhancements)
        {
            _staticDataService = staticDataService;
            _progressService = persistentData;
            _random = randomService;
            _playerEnhancements = playerEnhancements;
        }

        protected override void Initialize() => 
            RefreshBalance();

        protected override void SubscribeUpdates() => 
            ProgressService.PlayerProgress.Balance.Changed += RefreshBalance;

        protected override void Cleanup()
        {
            base.Cleanup();
            ProgressService.PlayerProgress.Balance.Changed -= RefreshBalance;
        }

        public void InitEnhancementViewers(HashSet<EnhancementStaticData> enhancements)
        {
            foreach (EnhancementStaticData enhancementData in enhancements)
            {
                EnhancementViewer enhancementViewer = Instantiate(_enhancementViewerPrefab, _content);
                enhancementViewer.Construct(_progressService, _playerEnhancements, enhancementData);
            }
        }

        public HashSet<EnhancementStaticData> InitNewEnhancementViewers()
        {
            HashSet<EnhancementStaticData> randomEnhancements = SelectRandomEnhancements();

            InitEnhancementViewers(randomEnhancements);

            return randomEnhancements;
        }

        private HashSet<EnhancementStaticData> SelectRandomEnhancements()
        {
            IList<EnhancementStaticData> enhancementsData = _staticDataService
                .GetAllDataByType<EnhancementId, EnhancementStaticData>().ToList();

            if (enhancementsData.Count < _enhancementsCount)
                throw new ArgumentOutOfRangeException(nameof(_enhancementsCount), "Not enough enhancements");
            
            HashSet<EnhancementStaticData> randomEnhancements = new(_enhancementsCount);
            
            for (int i = 0; i < _enhancementsCount;)
            {
                int randomEnhancementIndex = _random.Next(0, enhancementsData.Count);

                if (enhancementsData[randomEnhancementIndex] != null)
                {
                    randomEnhancements.Add(enhancementsData[randomEnhancementIndex]);
                    enhancementsData.RemoveAt(randomEnhancementIndex);
                    i++;
                }
            }

            return randomEnhancements;
        }

        private void RefreshBalance() => 
            _playerBalance.text = ProgressService.PlayerProgress.Balance.Coins.ToString();
    }
}