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

        public UnityAction<EnemyState> NeedTransit;
    }
}
