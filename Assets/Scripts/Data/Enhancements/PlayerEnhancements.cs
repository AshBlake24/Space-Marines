using System;

namespace Roguelike.Data.Enhancements
{
    [Serializable]
    public class PlayerEnhancements
    {
        public DamageEnhancement Damage;

        public PlayerEnhancements()
        {
            Damage = new DamageEnhancement();
        }
    }
}