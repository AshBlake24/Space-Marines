using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Logic;
using Roguelike.Logic.CharacterSelection;
using Roguelike.Logic.Interactables;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Buttons;
using Roguelike.UI.Elements;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class CharacterStats : BaseWindow
    {
        [Header("Character")]
        [SerializeField] private Image _characterIcon;
        [SerializeField] private HealthBar _health;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _description;
        
        [Header("Weapon")] 
        [SerializeField] private Image _startWeaponIcon;
        [SerializeField] private TextMeshProUGUI _weaponName;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _attackRate;
        [SerializeField] private TextMeshProUGUI _ammo;
        
        [Header("Skill")]
        [SerializeField] private Image _skillIcon;
        [SerializeField] private TextMeshProUGUI _skillName;
        [SerializeField] private TextMeshProUGUI _cooldown;
        [SerializeField] private TextMeshProUGUI _skillDescription;

        private IWeaponFactory _weaponFactory;
        private IStaticDataService _staticData;
        private CharacterStaticData _characterData;
        private CharacterSelectionMode _selectionMode;
        private SelectCharacterButton _selectCharacterButton;

        public void Construct(CharacterId characterId, CharacterSelectionMode selectionMode,
            IStaticDataService staticDataService, IWeaponFactory weaponFactory)
        {
            _characterData = staticDataService.GetCharacterData(characterId);
            _selectionMode = selectionMode;
            _weaponFactory = weaponFactory;
            _staticData = staticDataService;
        }

        protected override void Initialize()
        {
            WeaponStaticData startWeaponData = _staticData.GetWeaponData(_characterData.StartWeapon);
            SkillStaticData skillData = _staticData.GetSkillStaticData(_characterData.Skill);

            InitCharacter();
            InitWeapon(startWeaponData);
            InitSkill(skillData);

            _selectCharacterButton = GetComponentInChildren<SelectCharacterButton>();
            _selectCharacterButton.Construct(_characterData, ProgressService);
            _selectCharacterButton.CharacterSelected += OnCharacterSelected;
        }

        private void InitCharacter()
        {
            _characterName.text = _characterData.Id.ToString();
            _characterIcon.sprite = _characterData.Icon;
            _health.SetValue(_characterData.MaxHealth, _characterData.MaxHealth);
            _description.text = _characterData.Desctription;
        }

        private void InitWeapon(WeaponStaticData startWeaponData)
        {
            _startWeaponIcon.sprite = startWeaponData.Icon;
            _weaponName.text = startWeaponData.Name;
            _attackRate.text = $"Attack Rate: {startWeaponData.AttackRate}s";

            if (startWeaponData is RangedWeaponStaticData rangedWeaponData)
            {
                ProjectileStaticData projectileData = _staticData.GetProjectileData(rangedWeaponData.Projectile.Id);
                
                _damage.text = $"Damage: {projectileData.Damage}";
                _ammo.text = $"Ammo: {rangedWeaponData.MaxAmmo}";
            }
        }

        private void InitSkill(SkillStaticData skillData)
        {
            _skillIcon.sprite = skillData.Icon;
            _skillName.text = skillData.Id.ToString();
            _skillDescription.text = skillData.Description;
            _cooldown.text = $"Cooldown: {skillData.SkillCooldown}s";
        }

        private void OnCharacterSelected()
        {
            IWeapon startWeapon = _weaponFactory.CreateWeapon(_characterData.StartWeapon);
            ProgressService.PlayerProgress.PlayerWeapons = new PlayerWeapons(
                startWeapon,
                _characterData.MaxWeaponsCount);
            _selectionMode.OnCharacterSelected();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _selectCharacterButton.CharacterSelected -= OnCharacterSelected;
            _selectionMode.ZoomOut();
        }
    }
}