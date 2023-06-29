using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Regions
{
    public class RegionSelectionWindow : BaseWindow
    {
        private readonly List<RegionViewer> _viewers = new();
        
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Transform _content;
        [SerializeField] private RegionViewer _regionViewerPrefab;

        [Header("Start Button")]
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _startButtonLabel;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;

        private IStaticDataService _staticDataService;

        public event Action<RegionSelectionWindow, RegionStaticData> RegionSelected;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitRegionViewers();
            SetStartButtonState();
        }

        protected override void SubscribeUpdates() => 
            _startButton.onClick.AddListener(OnStartButtonClick);

        protected override void Cleanup()
        {
            base.Cleanup();

            foreach (RegionViewer regionViewer in _viewers)
                regionViewer.ToggleClicked -= OnToggleClicked;
            
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            TimeService.ResumeGame();
        }

        private void InitRegionViewers()
        {
            IEnumerable<RegionStaticData> regionsData = _staticDataService.GetAllDataByType<RegionId, RegionStaticData>();

            foreach (RegionStaticData regionData in regionsData)
            {
                RegionViewer regionViewer = Instantiate(_regionViewerPrefab, _content);
                regionViewer.Construct(regionData);
                regionViewer.ToggleClicked += OnToggleClicked;
                _toggleGroup.RegisterToggle(regionViewer.Toggle);
                _viewers.Add(regionViewer);
            }
        }

        private void SetStartButtonState()
        {
            if (_toggleGroup.AnyTogglesOn())
            {
                _startButton.interactable = true;
                _startButtonLabel.color = _activeColor;
            }
            else
            {
                _startButton.interactable = false;
                _startButtonLabel.color = _inactiveColor;  
            }
        }

        private void OnToggleClicked(bool isActive)
        {
            SetStartButtonState();
        }

        private void OnStartButtonClick()
        {
            RegionViewer regionViewer = _viewers.SingleOrDefault(viewer => viewer.Toggle.isOn);

            if (regionViewer != null)
                RegionSelected?.Invoke(this, regionViewer.RegionData);
            else
                throw new ArgumentNullException(nameof(regionViewer));
        }
    }
}