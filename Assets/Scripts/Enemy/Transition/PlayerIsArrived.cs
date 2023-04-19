using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roguelike.Player;

namespace Roguelike.Enemy
{
    public class PlayerIsArrived : Transition
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerComponent>(out PlayerComponent player))
            {
                NeedTransit?.Invoke(targetState);
            }
        }
    }
}
