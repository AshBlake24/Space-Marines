using System;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Logic.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private List<Button> _closeButtons;

        protected IPersistentDataService ProgressService;
        protected ITimeService TimeService;
        public event Action<BaseWindow> Closed;

        public void Construct(IPersistentDataService progressService, ITimeService timeService)
        {
            ProgressService = progressService;
            TimeService = timeService;
        }

        private void Awake() => OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => Cleanup();

        private void OnAwake()
        {
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.AddListener(OnClose);
        }

        private void OnClose()
        {
            Closed?.Invoke(this);
            
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(OnClose);

            Destroy(gameObject);
        }

        protected virtual void Initialize() {}

        protected virtual void SubscribeUpdates() {}

        protected virtual void Cleanup() {}
    }
}