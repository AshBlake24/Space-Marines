using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HorizontalAxis = "Horizontal";
        protected const string VerticalAxis = "Vertical";

        public abstract Vector2 Axis { get; }
        public abstract bool IsAttackButtonUp();
    }
}