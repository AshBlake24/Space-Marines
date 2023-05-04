using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class GameOverStageViewer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private TextMeshProUGUI _stageLabel;
        
        public void Construct(int currentStage, Sprite characterIcon, string stageLabel)
        {
            _slider.value = currentStage;
            _characterIcon.sprite = characterIcon;
            _stageLabel.text = stageLabel;
        }
    }
}