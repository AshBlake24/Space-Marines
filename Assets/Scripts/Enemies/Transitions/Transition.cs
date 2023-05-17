using Roguelike.Enemies.EnemyStates;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Enemies.Transitions
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] protected EnemyState targetState;

        protected bool isNeedTransit;

        protected EnemyState currentState;

        public EnemyState TargetState => targetState;

        public UnityAction<EnemyState> NeedTransit;
    }
}
