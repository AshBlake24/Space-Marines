using System;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    public abstract class PowerupEffect : ScriptableObject
    {
        public abstract bool TryApply(GameObject target, Action onComplete);
    }
}