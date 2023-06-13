using System;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Confirmations
{
    public abstract class ConfirmationWindow : BaseWindow
    {
        [SerializeField] protected Button _confirmButton;

        protected IStaticDataService StaticData;
        protected ISceneLoadingService SceneLoadingService;

        public event Action Confirmed;

        public void Construct(IStaticDataService staticData, ISceneLoadingService sceneLoadingService)
        {
            StaticData = staticData;
            SceneLoadingService = sceneLoadingService;
        }
        
        protected override void Initialize()
        {
            TimeService.PauseGame();
            _confirmButton.onClick.AddListener(OnConfirm);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _confirmButton.onClick.RemoveAllListeners();
        }

        protected virtual void OnConfirm() => Confirmed?.Invoke();
    }
}