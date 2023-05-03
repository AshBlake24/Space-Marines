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

        private CharacterStaticData _characterData;
        private IPersistentDataService _progressService;

        public event Action CharacterSelected;

        public void Construct(CharacterStaticData characterData, IPersistentDataService progressService)
        {
            _characterData = characterData;
            _progressService = progressService;
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            _progressService.PlayerProgress.Character = _characterData.Id;
            _progressService.PlayerProgress.State.MaxHealth = _characterData.MaxHealth;
            _progressService.PlayerProgress.State.ResetState();
            
            CharacterSelected?.Invoke();
        }
    }
}