using System.Collections;
using DG.Tweening;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Logic.Doors
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private PlayerTriggerObserver[] _triggers;
        [SerializeField] private Vector3 _openedPosition;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _openStateTime;

        private bool _isOpen;
        private Vector3 _defaultPosition;
        private Coroutine _closingCoroutine;

        private void OnEnable()
        {
            foreach (PlayerTriggerObserver trigger in _triggers)
                trigger.Triggered += OnTriggered;
        }

        private void Start()
        {
            _defaultPosition = transform.localPosition;
            _isOpen = false;
        }

        private void Open()
        {
            transform.DOLocalMove(_openedPosition, _animationDuration)
                .From(transform.localPosition)
                .SetEase(Ease.InOutQuad);

            _isOpen = true;
        }

        private void Close()
        {
            transform.DOLocalMove(_defaultPosition, _animationDuration)
                .From(transform.localPosition)
                .SetEase(Ease.InOutQuad);

            _isOpen = false;
        }

        private void OnTriggered()
        {
            if (_isOpen == false)
                Open();

            RestartCoroutine();
        }

        private void RestartCoroutine()
        {
            if (_closingCoroutine != null)
                StopCoroutine(_closingCoroutine);
            
            _closingCoroutine = StartCoroutine(CloseAfterDelay());
        }

        private IEnumerator CloseAfterDelay()
        {
            yield return Helpers.GetTime(_openStateTime);

            _closingCoroutine = null;
            Close();
        }
    }
}