using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public override Vector2 Axis => 
            new (SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
    }
}