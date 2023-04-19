using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Level
{
    public class EnterTriger : MonoBehaviour
    {
        public event UnityAction<PlayerComponent> PlayerHasEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerComponent>(out PlayerComponent player))
            {
                PlayerHasEntered?.Invoke(player);
                gameObject.SetActive(false);
            }
        }
    }
}
