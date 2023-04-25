using UnityEngine;

namespace Roguelike.Logic
{
    public interface IInteractable
    {
        GameObject gameObject { get ; } 
        void Interact();
    }
}