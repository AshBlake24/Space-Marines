using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Roguelike.Enemies.Traps
{
    public class SpearTrap : MonoBehaviour
    {
        [SerializeField] private Spear[] _spears;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _depth = 1.75f;
        [SerializeField] private float _switchTimer = 1.5f;
        [SerializeField] private float _switchingAnimationDuration = 0.25f;

        private Tween _tween;
        private Vector3 _onPosition;
        private Vector3 _offPosition;
        private bool _isActive;
        private float _timer;

        private void Start() => 
            InitSpears();

        private void OnValidate()
        {
            if (_switchingAnimationDuration > _switchTimer)
                _switchingAnimationDuration = _switchTimer;
        }

        private void OnDisable() => _tween.Kill();

        private void InitSpears()
        {
            _onPosition = transform.position;
            _offPosition = transform.position;
            _offPosition.y -= _depth;
            transform.position = _offPosition;
            _isActive = false;
            
            foreach (Spear spear in _spears)
                spear.Init(_damage);
            
            SwitchColliders();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _switchTimer)
            {
                SwitchState();
                _timer = 0f;
            }
        }

        private void SwitchState()
        {
            _isActive = !_isActive;

            Vector3 targetPosition = _isActive 
                ? _onPosition 
                : _offPosition;

            _tween = transform.DOMove(targetPosition, _switchingAnimationDuration)
                .SetEase(Ease.OutBounce);

            SwitchColliders();
        }

        private void SwitchColliders()
        {
            foreach (Spear spear in _spears)
                spear.SwitchState(_isActive);
        }
    }
}