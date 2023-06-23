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
        [SerializeField] private StatisticsField _totalPlayerScore;
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
            InitFavouriteWeapon();
            InitFavouriteCharacter();
            InitFavouriteEnhancement();
        }
        
        private void InitFavouriteWeapon()
        {
            WeaponId favouriteWeaponId = _statistics.Favourites.FavouriteWeapon;
            
            if (favouriteWeaponId != WeaponId.Unknown)
            {
                WeaponStaticData weaponData = _staticDataService
                    .GetDataById<WeaponId, WeaponStaticData>(favouriteWeaponId);
        
                _weaponIcon.sprite = weaponData.Icon;
                _weaponRarity.color = _rarityColors[weaponData.Rarity];
            }
            else
            {
                _weaponIcon.gameObject.SetActive(false);
                _weaponRarity.gameObject.SetActive(false);
            }
        }
        
        private void InitFavouriteEnhancement()
        {
            EnhancementId favouriteEnhancementId = _statistics.Favourites.FavouriteEnhancement;

            if (favouriteEnhancementId != EnhancementId.Unknown)
            {
                EnhancementStaticData enhancementData = _staticDataService
                    .GetDataById<EnhancementId, EnhancementStaticData>(favouriteEnhancementId);
        
                _enhancementIcon.sprite = enhancementData.Icon;
            }
            else
            {
                _enhancementIcon.gameObject.SetActive(false);
            }
        }
        
        private void InitFavouriteCharacter()
        {
            CharacterStaticData characterData = _staticDataService
                .GetDataById<CharacterId, CharacterStaticData>(_statistics.Favourites.FavouriteCharacter);
        
            _characterIcon.sprite = characterData.Icon;
        }
        
        private void InitStatisticsData()
        {
            _totalPlayerScore.SetStatsValue(_statistics.PlayerScore);
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