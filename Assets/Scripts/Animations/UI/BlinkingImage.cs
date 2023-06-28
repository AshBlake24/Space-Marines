using UnityEngine;

namespace Roguelike.Animations.UI
{
    public class BlinkingImage : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _fadingValue;
        private bool _decreasing;

        private void Start()
        {
            _fadingValue = 1 / _duration;
        }

        private void Update()
        {
            if (_decreasing)
                Fade();
            else
                Show();
        }

        private void Show()
        {
            _canvasGroup.alpha += _fadingValue * Time.unscaledDeltaTime;

            if (_canvasGroup.alpha >= 1)
                _decreasing = true;
        }

        private void Fade()
        {
            _canvasGroup.alpha -= _fadingValue * Time.unscaledDeltaTime;

            if (_canvasGroup.alpha <= 0)
                _decreasing = false;
        }
    }
}