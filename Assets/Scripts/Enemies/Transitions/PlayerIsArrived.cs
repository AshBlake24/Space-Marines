using UnityEngine;
using Roguelike.Player;

namespace Roguelike.Enemies.Transitions
{
    public class PlayerIsArrived : Transition
    {
        private void OnTriggerEnter(Collider other)
        {
            TryTakeDamage(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TryTakeDamage(other);
        }

        private void TryTakeDamage(Collider other)
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
                NeedTransit?.Invoke(targetState);
        }
    }
}
