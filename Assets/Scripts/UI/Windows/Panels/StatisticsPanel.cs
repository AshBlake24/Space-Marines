using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Enhancements;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Panels
{
    public class StatisticsPanel : MonoBehaviour
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
        private IStaticDataService _staticDataService;
        private Dictionary<RarityId, Color> _rarityColors;
        private Statistics _statistics;

        public void Construct(IPersistentDataService persistentData, IStaticDataService staticDataService)
        {
            _persistentData = persistentData;
            _staticDataService = staticDataService;
            _statistics = _persistentData.PlayerProgress.Statistics;
            _rarityColors = _staticDataService.GetAllDataByType<RarityId, RarityStaticData>()
                .ToDictionary(data => data.Id, data => data.Color);
        }

        public void InitStats()
        {
            InitStatisticsData();
            // InitFavouriteWeapon();
            // InitFavouriteCharacter();
            // InitFavouriteEnhancement();
        }

        // private void InitFavouriteEnhancement()
        // {
        //     if (_statistics.Favourites.Enhancement != null)
        //     {
        //         EnhancementStaticData enhancementData = _staticDataService
        //             .GetDataById<EnhancementId, EnhancementStaticData>(_statistics.Favourites.Enhancement);
        //
        //         _characterIcon.sprite = enhancementData.Icon;
        //     }
        // }
        //
        // private void InitFavouriteCharacter()
        // {
        //     if (_statistics.Favourites.Character != null)
        //     {
        //         CharacterStaticData characterData = _staticDataService
        //             .GetDataById<CharacterId, CharacterStaticData>(_statistics.Favourites.Character);
        //
        //         _characterIcon.sprite = characterData.Icon;
        //     }
        // }
        //
        // private void InitFavouriteWeapon()
        // {
        //     if (_statistics.Favourites.Weapon != null)
        //     {
        //         WeaponStaticData weaponData = _staticDataService
        //             .GetDataById<WeaponId, WeaponStaticData>(_statistics.Favourites.Weapon);
        //
        //         _weaponIcon.sprite = weaponData.Icon;
        //         _weaponRarity.color = _rarityColors[weaponData.Rarity];
        //     }
        // }

        private void InitStatisticsData()
        {
            _monstersKilled.SetStatsValue(_statistics.KillData.OverallKilledMonsters);
            _bossesKilled.SetStatsValue(_statistics.KillData.OverallKilledBosses);
            _coinsCollected.SetStatsValue(_statistics.CollectablesData.CoinsCollected);
            _coinsSpentOnEnhancements.SetStatsValue(_statistics.CollectablesData.CoinsSpentOnEnhancements);
            _enhancementsBought.SetStatsValue(_statistics.CollectablesData.EnhancementsBought);
            _healsPickedUp.SetStatsValue(_statistics.CollectablesData.FirstAidKitsCollected);
            _powerupsPickedUp.SetStatsValue(_statistics.CollectablesData.PowerupsCollected);
            _chestsOpened.SetStatsValue(_statistics.CollectablesData.ChestsOpened);
        }
    }
}