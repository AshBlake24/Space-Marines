using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
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

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        
        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void Cleanup() { }
    }
}