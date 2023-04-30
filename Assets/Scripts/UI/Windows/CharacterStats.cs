using Roguelike.Logic.Cameras;
using Roguelike.StaticData.Characters;
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

        private CharacterStaticData _characterData;
        private CharacterSelectionMode _selectionMode;

        public void Construct(CharacterStaticData characterData, CharacterSelectionMode selectionMode)
        {
            _characterData = characterData;
            _selectionMode = selectionMode;
        }

        protected override void Initialize()
        {
            _title.text = _characterData.Id.ToString();
            _characterIcon.sprite = _characterData.Icon;
            _description = _characterData.Desctription;
            _health.SetValue(_characterData.MaxHealth, _characterData.MaxHealth);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _selectionMode.ZoomOut();
        }
    }
}