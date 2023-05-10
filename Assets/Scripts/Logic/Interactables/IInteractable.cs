using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public interface IInteractable
    {
        Outline Outline { get; }
        bool IsActive { get; }
        void Interact(GameObject interactor);
    }
}