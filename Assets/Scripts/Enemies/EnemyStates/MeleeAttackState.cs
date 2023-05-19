using Roguelike.Enemies;
using System.Collections;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class MeleeAttackState : EnemyState
    {
        public override void Enter(Enemy enemy)
        {
            base.Enter(enemy);

            enemy.Target.TakeDamage(enemy.Damage);
        }
    }
}