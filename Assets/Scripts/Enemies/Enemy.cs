using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using System;

namespace Roguelike.Enemies
{
    public class Enemy
    {
        private int _maxBulletInBurst;
        public EnemyId Id { get; private set; }
        public int Damage { get; private set; }
        public float Danger { get; private set; }
        public float Speed { get; private set; }
        public float ChargeSpeedMultiplication { get; private set; }
        public float AttackColldown { get; private set; }
        public float LifeTime { get; private set; }
        public int BulletInBurst { get; private set; }
        public int Coins { get; private set; }

        public EnemyHealth Health {get; private set;}

        public PlayerHealth Target { get; private set; }

        public event Action NeedReloaded;

        public Enemy(EnemyStaticData data, EnemyHealth health, PlayerHealth target) 
        {
            Id = data.Id;
            Damage = data.Damage;
            Danger = data.Danger;
            AttackColldown = data.AttackCooldown;
            LifeTime = data.LifeTime;
            BulletInBurst = data.BulletInBurst;
            _maxBulletInBurst = data.BulletInBurst;
            ChargeSpeedMultiplication = data.ChargeSpeedMultiplication;
            Speed = data.Speed;
            Health = health;
            Target = target;
            Coins = data.Coins;
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
