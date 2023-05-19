using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Level
{
    public class EnterTriger : MonoBehaviour
    {
        public event UnityAction<PlayerHealth> PlayerHasEntered;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                PlayerHasEntered?.Invoke(player);
                gameObject.SetActive(false);
            }
        }
    }
}
