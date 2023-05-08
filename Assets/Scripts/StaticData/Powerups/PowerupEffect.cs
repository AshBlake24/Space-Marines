using UnityEngine;

namespace Roguelike.StaticData.Powerups
{
    public abstract class PowerupEffect : ScriptableObject
    {
        public abstract bool TryApply(GameObject target);
    }
}