using System;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Logic
{
    [RequireComponent(typeof(Collider))]
    public class PlayerTriggerObserver : MonoBehaviour
    {
        public event Action Triggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth player))
                Triggered?.Invoke();
        }
    }
}