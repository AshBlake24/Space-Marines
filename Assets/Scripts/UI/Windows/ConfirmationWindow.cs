using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public abstract class ConfirmationWindow : BaseWindow
    {
        [SerializeField] protected Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private string _context;

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