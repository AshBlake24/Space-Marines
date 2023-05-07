using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public class ItemPickUper : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                Debug.Log($"PickUp Item {gameObject.name}");
                Destroy(gameObject);
            }
        }
    }
}
