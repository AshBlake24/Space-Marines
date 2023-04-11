using Roguelike.Infrastructure.Services;
using UnityEngine;

namespace Roguelike.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
    }
}