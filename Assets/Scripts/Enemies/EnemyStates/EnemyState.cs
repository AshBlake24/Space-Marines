using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Roguelike.Enemies.Transitions;

namespace Roguelike.Enemies.EnemyStates
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected Enemy enemy;

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

        public virtual void Enter(Enemy curentEnemy)
        {
            enabled = true;
            enemy = curentEnemy;
        }

        public virtual void Exit(EnemyState nextState) 
        {
            enabled = false;
            StateFinished?.Invoke(nextState);
        }
    }
}
