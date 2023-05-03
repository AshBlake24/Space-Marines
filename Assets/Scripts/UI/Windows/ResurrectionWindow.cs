using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class ResurrectionWindow : BaseWindow
    {
        private const float FadeValue = 0.25f;
        
        [SerializeField] private Button _ressurectButton;
        
        private Image[] _ressurectButtonImages;
        
        protected override void Initialize()
        {
            InitResurrect();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _ressurectButton.onClick.RemoveAllListeners();
        }

        private void InitResurrect()
        {
            _ressurectButtonImages = _ressurectButton.GetComponentsInChildren<Image>();
            
            if (ProgressService.PlayerProgress.State.HasResurrected)
            {
                _ressurectButton.interactable = false;

                foreach (Image image in _ressurectButtonImages)
                {
                    image.color = new Color(
                        image.color.r - FadeValue,
                        image.color.g - FadeValue,
                        image.color.b - FadeValue,
                        image.color.a);
                }
            }
            else
            {
                _ressurectButton.onClick.AddListener(OnResurrect);
            }
        }

        private void OnResurrect()
        {
            // todo show ad & resurrect player
            
            throw new System.NotImplementedException();
        }
    }
}