using System;

namespace Roguelike.Data.Enhancements
{
    [Serializable]
    public class DamageEnhancement
    {
        public int Value;

        public event Action Changed;

        public DamageEnhancement()
        {
            Value = 0;
        }

        public void Upgrade(int value)
        {
            if (value <= Value)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    "The value after the upgrade must be higher than the previous value");
            }

            Value = value;
            Changed?.Invoke();
        }
    }
}