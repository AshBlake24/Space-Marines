using Roguelike.Player;
using UnityEngine.AI;

namespace Roguelike.Enemies.Transitions
{
    public class TargetIsNotInRange: Transition
    {
        protected const int MinCornersCount = 2;

        protected PlayerHealth _target;
        protected NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            if (_target == null)
                _target = GetComponent<EnemyStateMachine>().Enemy.Target;
        }
        private void Update()
        {
            if (_target == null)
                return;

            CheackLineOfSight();
        }

        protected virtual void CheackLineOfSight()
        {
            _agent.SetDestination(_target.transform.position);

            if (_agent.path.corners.Length > MinCornersCount)
                NeedTransit?.Invoke(targetState);
        }
    }
}
