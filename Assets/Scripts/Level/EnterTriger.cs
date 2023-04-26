using Roguelike.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Level
{
    public class EnterTriger : MonoBehaviour
    {
        public event UnityAction<PlayerHealth> PlayerHasEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                PlayerHasEntered?.Invoke(player);
                gameObject.SetActive(false);
            }
        }
    }
}
