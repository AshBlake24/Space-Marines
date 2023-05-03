using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public abstract class ConfirmationWindow : BaseWindow
    {
        [SerializeField] protected Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField, TextArea(minLines: 1,maxLines: 3)] private string _context;

        protected IStaticDataService StaticData;
        protected ISceneLoadingService SceneLoadingService;

        public void Construct(IStaticDataService staticData, ISceneLoadingService sceneLoadingService)
        {
            StaticData = staticData;
            SceneLoadingService = sceneLoadingService;
        }
        
        protected override void Initialize()
        {
            _description.text = _context;
            _confirmButton.onClick.AddListener(OnConfirm);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _confirmButton.onClick.RemoveAllListeners();
        }

        protected abstract void OnConfirm();
    }
}