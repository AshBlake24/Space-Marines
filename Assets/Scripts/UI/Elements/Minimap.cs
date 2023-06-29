using Roguelike.Localization;
using Roguelike.StaticData.Levels;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stageLabel;

        public void Construct(StageId currentStage)
        {
            _stageLabel.text = string.Format(LocalizedConstants.Stage.Value, currentStage.ToLabel());
        }
    }
}