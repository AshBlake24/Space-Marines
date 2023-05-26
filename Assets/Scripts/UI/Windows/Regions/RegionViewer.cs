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
        [SerializeField] private TextMeshProUGUI _stagesCount;
        [SerializeField] private Image _icon;

        public event Action<bool> ToggleClicked;

        public Toggle Toggle => _toggle;
        public RegionStaticData RegionData { get; private set; }

        public void Construct(RegionStaticData regionData)
        {
            _toggle.isOn = false;
            RegionData = regionData;
            _name.text = RegionData.Name;
            _stagesCount.text = RegionData.Stages.Length.ToString();
            _toggle.onValueChanged.AddListener(OnToggleClick);
        }

        private void OnDestroy() => 
            _toggle.onValueChanged.RemoveListener(OnToggleClick);

        private void OnToggleClick(bool isActive) => 
            ToggleClicked?.Invoke(isActive);
    }
}