using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class GameOverStageViewer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _characterIcon;
        
        public void Construct(int currentStage, Sprite characterIcon)
        {
            _slider.value = currentStage;
            _characterIcon.sprite = characterIcon;
        }
    }
}