using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using System;

namespace Roguelike.Enemies
{
    public class Enemy
    {
        private int _maxBulletInBurst;
        public int Damage { get; private set; }
        public float Danger { get; private set; }
        public float Speed { get; private set; }
        public float ChargeSpeedMultiplication { get; private set; }
        public float AttackColldown { get; private set; }
        public float AttackSpeed { get; private set; }
        public int BulletInBurst { get; private set; }

        public EnemyHealth Health {get; private set;}

        public PlayerHealth Target { get; private set; }

        public event Action NeedReloaded;

        public Enemy(EnemyStaticData data, EnemyHealth health, PlayerHealth target) 
        {
            Damage = data.Damage;
            Danger = data.Danger;
            AttackColldown = data.AttackCooldown;
            AttackSpeed = data.AttackSpeed;
            BulletInBurst = data.BulletInBurst;
            _maxBulletInBurst = data.BulletInBurst;
            ChargeSpeedMultiplication = data.ChargeSpeedMultiplication;
            Speed = data.Speed;
            Health = health;
            Target = target;
        }

        public void RangeAttack()
        {
            if (BulletInBurst > 0)
                BulletInBurst--;

            if (BulletInBurst <= 0)
            {
                NeedReloaded?.Invoke();
                Reload();
            }
        }

        private void Reload()
        {
            BulletInBurst = _maxBulletInBurst;
        }
    }
}
