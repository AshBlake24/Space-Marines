using Roguelike.Player;
using Roguelike.StaticData.Enemies;

namespace Roguelike.Enemies
{
    public class Enemy
    {
        public int Damage { get; private set; }

        public float Danger { get; private set; }

        public EnemyHealth Health {get; private set;}

        public PlayerHealth Target { get; private set; }

        public Enemy(EnemyStaticData data, EnemyHealth health, PlayerHealth target) 
        {
            Damage = data.Damage;
            Danger = data.Danger;
            Health = health;
            Target = target;
        }
    }
}
