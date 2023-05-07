using UnityEngine;

namespace Roguelike.Loot
{
    public class LootView : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        public Transform Container => _container;

        public void ClearContainer()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}