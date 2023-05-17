using Roguelike.Player;
using UnityEngine.AI;

namespace Roguelike.Enemies.Transitions
{
    public class TargetIsNotInRange : Transition
    {
        private const int MinCornersCount = 2;

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
            if (_target == null)
                return;

            _agent.SetDestination(_target.transform.position);

            if (_agent.path.corners.Length > MinCornersCount)
                NeedTransit?.Invoke(targetState);
        }
    }
}
