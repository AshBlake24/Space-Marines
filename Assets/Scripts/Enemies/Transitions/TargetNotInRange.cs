using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemies.Transitions
{
    public class TargetNotInRange : Transition
    {
        private bool _isPlayerInRange;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
                _isPlayerInRange = true;
        }

        public void OnTriggerExit(Collider other) 
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
                _isPlayerInRange = false;
        }

        public bool TryFinishState()
        {
            if (_isPlayerInRange == false)
            {
                NeedTransit?.Invoke(targetState);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}