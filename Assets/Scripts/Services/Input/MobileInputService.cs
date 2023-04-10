using UnityEngine;

namespace Roguelike.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => 
            new (SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
    }
}