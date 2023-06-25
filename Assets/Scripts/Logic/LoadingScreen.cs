using System.Collections;
using Roguelike.Localization;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;

namespace Roguelike.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private LocalizedString _localizedString;
        [SerializeField] private float _fadeTime;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            _textField.text = _localizedString.Value;
        }

        public void Hide()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (_canvasGroup.alpha > _fadeTime)
            {
                _canvasGroup.alpha -= _fadeTime;
                yield return Helpers.GetTime(_fadeTime);
            }

            gameObject.SetActive(false);
        }
    }
}