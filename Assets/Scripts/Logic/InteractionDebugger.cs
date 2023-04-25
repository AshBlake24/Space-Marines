using UnityEngine;

namespace Roguelike.Logic
{
    public class InteractionDebugger : MonoBehaviour, IInteractable
    {
        public void Interact() => 
            Debug.Log($"{gameObject.name} interacted!");
    }
}