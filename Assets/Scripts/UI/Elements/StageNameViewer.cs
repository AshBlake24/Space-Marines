using System.Collections;
using Roguelike.Localization;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class StageNameViewer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private LocalizedString _localizedString;
        [SerializeField, Range(0f, 5f)] private float _holdTime;
        [SerializeField, Range(0f, 1f)] private float _fadeTime;

        public void Show(string stage)
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            _textField.text = string.Format(_localizedString.Value, stage);
            StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return Helpers.GetTime(_holdTime);
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (_canvasGroup.alpha > _fadeTime)
            {
                _canvasGroup.alpha -= _fadeTime;
                yield return Helpers.GetTime(_fadeTime);
            }

            Destroy(gameObject);
        }
    }
}