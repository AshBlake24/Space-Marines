using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class RangedWeaponStatsViewer : MonoBehaviour
    {
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TextMeshProUGUI _weaponName;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _attackRate;
        [SerializeField] private TextMeshProUGUI _ammo;

        private IPersistentDataService _progressData;
        private RangedWeaponStaticData _weaponData;

        public void Construct(IPersistentDataService progressData, RangedWeaponStaticData rangedWeaponData)
        {
            _progressData = progressData;
            _weaponData = rangedWeaponData;
            
            Initialize();
        }

        private void Initialize()
        {
            _weaponIcon.sprite = _weaponData.Icon;
            InitWeaponStats();
        }

        private void InitWeaponStats()
        {
            _weaponName.text = _weaponData.Name;
            _attackRate.text = $"{_weaponData.AttackRate}";
            _damage.text = $"{_weaponData.Damage}x{_weaponData.BulletsPerShot}";

            AmmoData ammoData = _progressData.PlayerProgress.PlayerWeapons.RangedWeaponsData
                .SingleOrDefault(data => data.ID == _weaponData.Id)?.AmmoData;

            _ammo.text = ammoData != null
                ? $"{ammoData.CurrentAmmo}/{ammoData.MaxAmmo}"
                : $"{_weaponData.MaxAmmo}/{_weaponData.MaxAmmo}";
        }
    }
}