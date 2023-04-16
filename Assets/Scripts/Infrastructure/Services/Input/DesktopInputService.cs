using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public class DesktopInputService : InputService
    {
        public override Vector2 Axis => 
            new (UnityEngine.Input.GetAxisRaw(HorizontalAxis), UnityEngine.Input.GetAxisRaw(VerticalAxis));

        public override bool IsAttackButtonUp() => UnityEngine.Input.GetButton("Fire1");
    }
}