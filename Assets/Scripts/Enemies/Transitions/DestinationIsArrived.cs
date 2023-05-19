﻿using UnityEngine.AI;

namespace Roguelike.Enemies.Transitions
{
    public class DestinationIsArrived : Transition
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_agent.destination == transform.position)
                NeedTransit?.Invoke(targetState);
        }
    }
}