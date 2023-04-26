using UnityEngine;
using Roguelike.Player;

namespace Roguelike.Enemies.Transitions
{
    public class PlayerIsArrived : Transition
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
                NeedTransit?.Invoke(targetState);
        }
    }
}
