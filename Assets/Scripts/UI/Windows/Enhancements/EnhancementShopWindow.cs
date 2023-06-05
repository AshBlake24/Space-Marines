using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
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
        [SerializeField] private EnhancementViewer _enhancementViewer;
        [SerializeField] private TextMeshProUGUI _playerBalance;
        [SerializeField, Range(1, 3)] private int _enhancementsCount;

        private IStaticDataService _staticDataService;
        private IPersistentDataService _progressService;
        private IRandomService _random;
        private PlayerEnhancements _playerEnhancements;
        private HashSet<EnhancementViewer> _viewers;

        public void Construct(IStaticDataService staticDataService, IPersistentDataService persistentData, IRandomService randomService,
            PlayerEnhancements playerEnhancements)
        {
            _staticDataService = staticDataService;
            _progressService = persistentData;
            _random = randomService;
            _playerEnhancements = playerEnhancements;
            _viewers = new HashSet<EnhancementViewer>(_enhancementsCount);
        }

        protected override void Initialize()
        {
            InitEnhancementViewers();
            RefreshBalance();
        }

        protected override void SubscribeUpdates() => 
            ProgressService.PlayerProgress.Balance.Changed += RefreshBalance;

        protected override void Cleanup()
        {
            base.Cleanup();
            ProgressService.PlayerProgress.Balance.Changed -= RefreshBalance;
        }

        private void InitEnhancementViewers()
        {
            EnhancementStaticData[] enhancementsData = _staticDataService
                .GetAllDataByType<EnhancementId, EnhancementStaticData>().ToArray();

            if (enhancementsData.Length >= _enhancementsCount)
            {
                IEnumerable<EnhancementStaticData> randomEnhancements = SelectRandomEnhancements(enhancementsData);

                foreach (EnhancementStaticData enhancementData in randomEnhancements)
                {
                    EnhancementViewer enhancementViewer = Instantiate(_enhancementViewer, _content);
                    
                    enhancementViewer.Construct(_progressService, _playerEnhancements, enhancementData);
                    
                    _viewers.Add(enhancementViewer);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(_enhancementsCount), "Not enough enhancements");
            }
        }

        private IEnumerable<EnhancementStaticData> SelectRandomEnhancements(IList<EnhancementStaticData> enhancementData)
        {
            EnhancementStaticData[] randomEnhancements = new EnhancementStaticData[_enhancementsCount];
            
            for (int i = 0; i < _enhancementsCount;)
            {
                int randomEnhancementIndex = _random.Next(0, enhancementData.Count);

                if (enhancementData[randomEnhancementIndex] != null)
                {
                    randomEnhancements[i] = enhancementData[randomEnhancementIndex];
                    enhancementData[randomEnhancementIndex] = null;
                    i++;
                }
            }

            return randomEnhancements;
        }
        
        private void RefreshBalance() => 
            _playerBalance.text = ProgressService.PlayerProgress.Balance.Coins.ToString();
    }
}