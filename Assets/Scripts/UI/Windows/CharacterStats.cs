using Roguelike.Infrastructure.Factory;
using Roguelike.Logic;
using Roguelike.StaticData.Characters;
using Roguelike.UI.Buttons;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class CharacterStats : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private Image _skillIcon; // todo
        [SerializeField] private Image _startWeaponIcon; // todo
        [SerializeField] private Button _selectButton;
        [SerializeField] private Elements.HealthBar _health;
        [SerializeField] private string _description;

        private IWeaponFactory _weaponFactory;
        private CharacterStaticData _characterData;
        private CharacterSelectionMode _selectionMode;
        private SelectCharacterButton _selectCharacterButton;

        public void Construct(CharacterStaticData characterData, CharacterSelectionMode selectionMode,
            IWeaponFactory weaponFactory)
        {
            _characterData = characterData;
            _selectionMode = selectionMode;
            _weaponFactory = weaponFactory;
        }

        protected override void Initialize()
        {
            _title.text = _characterData.Id.ToString();
            _characterIcon.sprite = _characterData.Icon;
            _description = _characterData.Desctription;
            _health.SetValue(_characterData.MaxHealth, _characterData.MaxHealth);

            _selectCharacterButton = GetComponentInChildren<SelectCharacterButton>();
            _selectCharacterButton.Construct(_characterData, ProgressService);
            _selectCharacterButton.CharacterSelected += OnCharacterSelected;
        }

        private void OnCharacterSelected()
        {
            IWeapon startWeapon = _weaponFactory.CreateWeapon(_characterData.StartWeapon);
            ProgressService.PlayerProgress.PlayerWeapons.InitializeStartWeapon(startWeapon);
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