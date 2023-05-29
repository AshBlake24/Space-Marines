using System;
using Roguelike.StaticData.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Regions
{
    public class RegionViewer : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _floorsCount;
        [SerializeField] private TextMeshProUGUI _stagesCount;
        [SerializeField] private Image _icon;
        [SerializeField] private RegionDangerViewer _dangerViewer;

        public event Action<bool> ToggleClicked;

        public Toggle Toggle => _toggle;
        public RegionStaticData RegionData { get; private set; }

        public void Construct(RegionStaticData regionData)
        {
            RegionData = regionData;
            InitRegionViewer();
            InitDangerViewer();
        }

        private void InitRegionViewer()
        {
            _toggle.isOn = false;
            _name.text = RegionData.Name;
            _icon.sprite = RegionData.Icon;
            _floorsCount.text = RegionData.Floors.Length.ToString();
            _stagesCount.text = RegionData.Floors[0].Stages.Length.ToString();
            _toggle.onValueChanged.AddListener(OnToggleClick);
        }

        private void InitDangerViewer() => 
            _dangerViewer.Init(RegionData.Difficulty);

        private void OnDestroy() => 
            _toggle.onValueChanged.RemoveListener(OnToggleClick);

        private void OnToggleClick(bool isActive) => 
            ToggleClicked?.Invoke(isActive);
    }
}