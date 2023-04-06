using System.Collections;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
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