using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Roguelike.Enemies.Transitions;
using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies.EnemyStates
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected Enemy enemy;
        protected EnemyAnimator animator;

        public event UnityAction<EnemyState> StateFinished;

        protected virtual void OnEnable()
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.NeedTransit += Exit;
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
                transition.NeedTransit -= Exit;
            }
        }

        public virtual void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            enabled = true;
            enemy = curentEnemy;
            animator = enemyAnimator;
        }

        public virtual void Exit(EnemyState nextState) 
        {
            enabled = false;
            StateFinished?.Invoke(nextState);
        }
    }
}
