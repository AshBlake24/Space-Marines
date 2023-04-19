using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Enemy
{
    public class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected PlayerComponent player;

        public event UnityAction<State> StateFinished;

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

        public virtual void Enter(PlayerComponent target)
        {
            player = target;
            enabled = true;
        }

        public virtual void Exit(State nextState) 
        {
            StateFinished?.Invoke(nextState);
            enabled = false;
        }
    }
}
