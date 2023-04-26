using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Items
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
