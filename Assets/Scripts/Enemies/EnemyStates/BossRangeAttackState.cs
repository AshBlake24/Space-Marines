using Roguelike.Roguelike.Enemies.Animators;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class BossRangeAttackState : EnemyState
    {
        [SerializeField] private List<RangeAttackState> _attackVariant;

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            int attackType = Random.Range(0, _attackVariant.Count);

            animator.SetTypeAttack(attackType);

            _attackVariant[attackType].Enter(curentEnemy, enemyAnimator);
        }
    }
}