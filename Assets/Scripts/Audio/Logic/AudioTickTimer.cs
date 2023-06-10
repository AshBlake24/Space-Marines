using System;
using UnityEngine;

namespace Roguelike.Audio.Logic
{
    public class AudioTickTimer : MonoBehaviour
    {
        public const float TickTime = 0.2f;

        private float _elapsedTime;
        
        public static event Action Tick;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= TickTime)
            {
                Tick?.Invoke();
                _elapsedTime = 0f;
            }
        }
    }
}