using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.UI.Windows.Enhancements
{
    public class EnhancementShopWindow : BaseWindow
    {
        [SerializeField] private Transform _content;
        [SerializeField] private EnhancementViewer _enhancementViewer;
        [SerializeField, Range(1, 3)] private int _enhancementsCount;

        private IStaticDataService _staticDataService;
        private IRandomService _random;

        public void Construct(IStaticDataService staticDataService, IRandomService randomService)
        {
            _staticDataService = staticDataService;
            _random = randomService;
        }

        protected override void Initialize() => 
            InitEnhancementViewers();

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
                    EnhancementData enhancementProgress = ProgressService.PlayerProgress.State.Enhancements
                        .SingleOrDefault(item => item.Id == enhancementData.Id);

                    if (enhancementProgress == null)
                        enhancementProgress = new EnhancementData(enhancementData.Id, 1);
                    
                    enhancementViewer.Init(enhancementData, enhancementProgress);
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
    }
}