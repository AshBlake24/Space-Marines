using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using Roguelike.Logic.CharacterSelection;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Buttons;
using Roguelike.UI.Elements;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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

        public void Construct(IStaticDataService staticDataService, IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            _staticData = staticDataService;
        }

        public void Init(CharacterId characterId, CharacterSelectionMode selectionMode)
        {
            _characterData = _staticData.GetDataById<CharacterId, CharacterStaticData>(characterId);
            _selectionMode = selectionMode;
        }

        protected override void Initialize()
        {
            WeaponStaticData startWeaponData = _staticData.GetDataById<WeaponId, WeaponStaticData>(_characterData.StartWeapon);
            SkillStaticData skillData = _staticData.GetDataById<SkillId, SkillStaticData>(_characterData.Skill);

            InitCharacter();
            InitSkill(skillData);
            InitWeapon(startWeaponData);

            _selectCharacterButton = GetComponentInChildren<SelectCharacterButton>();
            _selectCharacterButton.Construct(_characterData, ProgressService);
            _selectCharacterButton.CharacterSelected += OnCharacterSelected;
        }

        private void InitCharacter()
        {
            _characterName.text = _characterData.Name.Value;
            _characterIcon.sprite = _characterData.Icon;
            _health.SetValue(_characterData.MaxHealth, _characterData.MaxHealth);
            _description.text = _characterData.Description.Value;
        }

        private void InitSkill(SkillStaticData skillData)
        {
            _skillIcon.sprite = skillData.Icon;
            _skillName.text = skillData.Name.Value;
            _skillDescription.text = skillData.GetLocalizedDescription();
            _cooldown.text = $"{LocalizedConstants.Cooldown.Value}: {skillData.SkillCooldown}{LocalizedConstants.TimeInSeconds.Value}";
        }

        private void InitWeapon(WeaponStaticData startWeaponData)
        {
            _startWeaponIcon.sprite = startWeaponData.SquareIcon;
            _weaponName.text = startWeaponData.Name.Value;
            _attackRate.text = $"{LocalizedConstants.AttackRate.Value}: {startWeaponData.AttackRate}{LocalizedConstants.TimeInSeconds.Value}";
            _damage.text = $"{LocalizedConstants.Damage.Value}: {startWeaponData.Damage}";

            if (startWeaponData is RangedWeaponStaticData)
                _ammo.text = $"{LocalizedConstants.Ammo.Value}: {LocalizedConstants.Infinity.Value}";
        }

        private void OnCharacterSelected()
        {
            IWeapon startWeapon = _weaponFactory.CreateWeapon(_characterData.StartWeapon);
            ProgressService.PlayerProgress.PlayerWeapons = new PlayerWeapons(
                startWeapon,
                _characterData.MaxWeaponsCount);

            _selectionMode.OnCharacterSelected();

            Destroy(startWeapon as Object);
            Close();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _selectCharacterButton.CharacterSelected -= OnCharacterSelected;
            _selectionMode.ZoomOut();
        }
    }
}