using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public interface IInteractable
    {
        Outline Outline { get; }
        GameObject gameObject { get ; } 
        void Interact(GameObject interactor);
    }
}