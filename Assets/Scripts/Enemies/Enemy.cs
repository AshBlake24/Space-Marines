using Roguelike.Player;
using Roguelike.StaticData.Enemies;

namespace Roguelike.Enemies
{
    public class Enemy
    {
        public int Damage { get; private set; }

        public EnemyHealth Health {get; private set;}

        public PlayerHealth Target { get; private set; }

        public Enemy(EnemyStaticData data, EnemyHealth health, PlayerHealth target) 
        {
            Damage= data.Damage;
            Health = health;
            Target = target;
        }
    }
}
