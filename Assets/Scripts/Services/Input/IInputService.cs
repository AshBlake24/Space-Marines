using UnityEngine;

namespace Roguelike.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
    }
}