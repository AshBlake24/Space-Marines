using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public interface IInteractable
    {
        Outline Outline { get; }
        void Interact(GameObject interactor);
    }
}