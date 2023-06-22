using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Panels
{
    public class StatisticPanel : MonoBehaviour
    {
        [Header("Statistics")]
        [SerializeField] private StatisticsField _monstersKilled;
        [SerializeField] private StatisticsField _bossesKilled;
        [SerializeField] private StatisticsField _coinsCollected;
        [SerializeField] private StatisticsField _coinsSpentOnEnhancements;
        [SerializeField] private StatisticsField _enhancementsBought;
        [SerializeField] private StatisticsField _healsPickedUp;
        [SerializeField] private StatisticsField _powerupsPickedUp;
        [SerializeField] private StatisticsField _chestsOpened;

        [Header("Favourites")] 
        [SerializeField] private Image _enhancementIcon;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private Image _weaponRarity;
        
        private IPersistentDataService _persistentData;

        public void Construct(IPersistentDataService persistentData)
        {
            _persistentData = persistentData;
        }

        public void InitStats()
        {
            Statistics statistics = _persistentData.PlayerProgress.Statistics;
            
            _monstersKilled.SetStatsValue(statistics.KillData.OverallKilledMonsters);
            _bossesKilled.SetStatsValue(statistics.KillData.OverallKilledBosses);
            _coinsCollected.SetStatsValue(statistics.CoinsCollected);
            _coinsSpentOnEnhancements.SetStatsValue(0);//todo
            _enhancementsBought.SetStatsValue(statistics.EnhancementsBought);
            _healsPickedUp.SetStatsValue(0);//todo
            _powerupsPickedUp.SetStatsValue(0);//todo
            _chestsOpened.SetStatsValue(statistics.ChestsOpened);
        }
    }
}