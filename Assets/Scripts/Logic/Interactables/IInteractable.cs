using System;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public interface IInteractable
    {
        event Action Interacted;
        
        Outline Outline { get; }
        bool IsActive { get; }

        void Interact(GameObject interactor);
    }
}