using System;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SelectCharacterButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private CharacterId _characterId;
        private IPersistentDataService _progressService;

        public event Action CharacterSelected;

        public void Construct(CharacterId characterId, IPersistentDataService progressService)
        {
            _characterId = characterId;
            _progressService = progressService;
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            _progressService.PlayerProgress.Character = _characterId;
            CharacterSelected?.Invoke();
        }
    }
}