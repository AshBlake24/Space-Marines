
namespace Roguelike.Enemy
{
    public class EnemyModel
    {
        public float Health { get; private set; }
        public float Damage { get; private set; }

        public EnemyModel(float health, float damage)
        {
            Health = health;
            Damage = damage;
        }

        public void TakeDamage(float damage)
        {
            if (damage > 0)
                if (damage < Health)
                    Health -= damage;
                else
                    Health = 0;
        }
    }
}
