using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.GameOver
{
    public class GameOverStageViewer : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private StageView _hubView;
        [SerializeField] private StageView _stageView;
        [SerializeField] private StageView _bossStageView;
        [SerializeField] private RectTransform _parent;

        [Header("Current Stage Viewer")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private TextMeshProUGUI _stageLabel;

        private int _stagesCount;
        private float _elementsSpacing;
        private Vector3 _currentElementPosition;
        
        public void Construct(int currentStage, string stageLabel, int stagesCount, Sprite characterIcon)
        {
            _stagesCount = stagesCount;
            _parent.sizeDelta = new Vector2(_parent.rect.width, _hubView.RectTransform.rect.height);

            CalculateSpacing();
            ConstructStructure();
            InitCurrentStageViewer(currentStage, characterIcon, stageLabel);
        }

        private void InitCurrentStageViewer(int currentStage, Sprite characterIcon, string stageLabel)
        {
            _slider.value = currentStage;
            _characterIcon.sprite = characterIcon;
            _stageLabel.text = stageLabel;
        }

        private void ConstructStructure()
        {
            _currentElementPosition = Vector3.zero;
            
            StageView hubView = Instantiate(_hubView, _parent);
            hubView.RectTransform.localPosition = _currentElementPosition;
            UpdateCurrentElementPosition(hubView.RectTransform.rect.width);

            for (int i = 0; i < _stagesCount - 1; i++)
            {
                StageView stageView = Instantiate(_stageView, _parent);
                stageView.RectTransform.localPosition = _currentElementPosition;
                UpdateCurrentElementPosition(stageView.RectTransform.rect.width);
            }
            
            StageView bossStageView = Instantiate(_bossStageView, _parent);
            bossStageView.RectTransform.localPosition = _currentElementPosition;
        }

        private void UpdateCurrentElementPosition(float elementWidth) => 
            _currentElementPosition.x += _elementsSpacing + elementWidth;

        private void CalculateSpacing()
        {
            _elementsSpacing = _parent.rect.width;
            _elementsSpacing -= _hubView.RectTransform.rect.width;
            _elementsSpacing -= _stageView.RectTransform.rect.width * (_stagesCount - 1);
            _elementsSpacing -= _bossStageView.RectTransform.rect.width;
            _elementsSpacing /= _stagesCount;
        }
    }
}