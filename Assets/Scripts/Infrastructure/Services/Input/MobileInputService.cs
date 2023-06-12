using Roguelike.Infrastructure.AssetManagement;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string AttackButton = "Fire";

        public override Vector2 Axis => 
            new(SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));

        public override bool IsAttackButtonUp() => SimpleInput.GetButton(AttackButton);
    }
}