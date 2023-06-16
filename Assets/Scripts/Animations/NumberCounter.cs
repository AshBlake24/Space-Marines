using System;
using TMPro;
using UnityEngine;

namespace Roguelike.Animations
{
    public class NumberCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _number;
        [SerializeField] private int _countFPS = 30;
        [SerializeField] private float _duration = 3f;

        private int _newValue;
        private int _currentValue;
        private int _previousValue;
        private int _stepAmount;
        private float _waitingTime;
        private float _elapsedTime;
        private bool _isActive;

        public event Action<TextMeshProUGUI> NumberReached; 

        private void Start() => 
            _waitingTime = 1f / _countFPS;

        private void Update()
        {
            if (_isActive)
            {
                _elapsedTime += Time.unscaledDeltaTime;

                if (_elapsedTime >= _waitingTime)
                {
                    Count();
                    _elapsedTime = 0;
                }
            }
        }

        public void UpdateText(int newValue)
        {
            _isActive = true;
            _newValue = newValue;
            _previousValue = _currentValue;
            _elapsedTime = 0;
            _stepAmount = _newValue - _previousValue < 0 
                ? Mathf.FloorToInt((_newValue - _previousValue) / (_countFPS * _duration)) 
                : Mathf.CeilToInt((_newValue - _previousValue) / (_countFPS * _duration));
        }

        private void Count()
        {
            if (_previousValue < _newValue)
            {
                _previousValue += _stepAmount;

                if (_previousValue > _newValue)
                    _previousValue = _newValue;
            }
            else if (_previousValue > _newValue)
            {
                _previousValue += _stepAmount;

                if (_previousValue < _newValue)
                    _previousValue = _newValue;
            }
            else
            {
                _currentValue = _previousValue;
                _isActive = false;
                
                NumberReached?.Invoke(_number);
                
                return;
            }
            
            _number.SetText(_previousValue.ToString());
        }
    }
}