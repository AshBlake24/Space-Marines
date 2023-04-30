using Roguelike.Logic.Cameras;
using Roguelike.StaticData.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class CharacterStats : BaseWindow
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Elements.HealthBar _health;

        private CharacterStaticData _characterData;
        private CharacterSelectionMode _selectionMode;

        public void Construct(CharacterStaticData characterData, CharacterSelectionMode selectionMode)
        {
            _characterData = characterData;
            _selectionMode = selectionMode;
        }

        protected override void Cleanup()
        {
            _selectionMode.ZoomOut();
        }
    }
}