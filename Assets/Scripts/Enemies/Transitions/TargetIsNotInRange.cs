using Roguelike.Level;
using Roguelike.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.Transitions
{
    public class TargetIsNotInRange : Transition
    {
        private PlayerHealth _target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            if (_target == null)
                _target= GetComponent<EnemyStateMachine>().Enemy.Target;
        }

        private void Update()
        {
            if (_agent.remainingDistance <= Vector3.Distance(_target.transform.position, transform.position))
                NeedTransit?.Invoke(targetState);
        }
    }
}
