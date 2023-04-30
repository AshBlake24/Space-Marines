using System.Collections.Generic;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private List<Button> _closeButtons;

        protected IPersistentDataService ProgressService;

        public void Construct(IPersistentDataService progressService) => 
            ProgressService = progressService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake()
        {
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.AddListener(OnClose);
        }

        private void OnClose()
        {
            foreach (Button closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(OnClose);

            Destroy(gameObject);
        }

        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void Cleanup() { }
    }
}