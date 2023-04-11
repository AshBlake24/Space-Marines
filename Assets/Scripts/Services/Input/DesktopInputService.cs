using UnityEngine;

namespace Roguelike.Services.Input
{
    public class DesktopInputService : InputService
    {
        public override Vector2 Axis => 
            new (UnityEngine.Input.GetAxisRaw(HorizontalAxis), UnityEngine.Input.GetAxisRaw(VerticalAxis));
    }
}