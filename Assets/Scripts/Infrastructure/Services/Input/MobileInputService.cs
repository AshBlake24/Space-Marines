using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => 
            new (SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
        
        public override bool IsAttackButtonUp()
        {
            throw new System.NotImplementedException();
        }
    }
}