using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public class InteractionDebugger : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;

        public Outline Outline => _outline;

        public void Interact(GameObject interactor) => 
            Debug.Log($"{gameObject.name} interacted!");
    }
}