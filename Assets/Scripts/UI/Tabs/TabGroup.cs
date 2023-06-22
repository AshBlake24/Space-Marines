using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.UI.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _hoverColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private List<GameObject> _objectsToSwap;

        private List<TabButton> _tabButtons;
        private TabButton _selectedTab;

        private void Start()
        {
            if (_tabButtons is {Count: > 0})
            {
                ResetTabs();
                OnTabSelected(_tabButtons[0]);
            }
        }

        public void Subscribe(TabButton tabButton)
        {
            _tabButtons ??= new List<TabButton>();
            _tabButtons.Add(tabButton);
        }

        public void OnTabEnter(TabButton tabButton)
        {
            ResetTabs();
            
            if (_selectedTab == null || tabButton != _selectedTab) 
                tabButton.Text.color = _hoverColor;
        }

        public void OnTabSelected(TabButton tabButton)
        {
            ChangeSelectedTab(tabButton);
            ResetTabs();
            
            tabButton.Text.color = _activeColor;
            int index = tabButton.transform.GetSiblingIndex();

            SwapObjects(index);
        }

        public void OnTabExit(TabButton tabButton)
        {
            ResetTabs();
        }

        private void SwapObjects(int index)
        {
            for (int i = 0; i < _objectsToSwap.Count; i++)
                _objectsToSwap[i].SetActive(i == index);
        }

        private void ChangeSelectedTab(TabButton tabButton)
        {
            if (_selectedTab != null)
                _selectedTab.OnDeselected();

            _selectedTab = tabButton;
            _selectedTab.OnSelected();
        }

        private void ResetTabs()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                if (_selectedTab != null && tabButton == _selectedTab)
                    continue;
                
                tabButton.Text.color = _inactiveColor;
            }
        }
    }
}