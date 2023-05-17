using Roguelike.Enemies;
using Roguelike.Level;
using Roguelike.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.Transitions
{
    public class TargetFinded: Transition
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
                _target = GetComponent<EnemyStateMachine>().Enemy.Target;
        }
        private void Update()
        {
            if (_target == null)
                return;

            if (_agent.path.corners.Length == MinCornersCount)
                NeedTransit?.Invoke(targetState);
        }
    }
}
