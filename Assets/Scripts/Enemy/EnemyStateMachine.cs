using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roguelike.Player;
using System;
using UnityEngine.Events;

namespace Roguelike
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _startState;
        [SerializeField] private Enemy _enemy;

        private PlayerComponent _player;
        private State _currentState;

        public event UnityAction<EnemyStateMachine> EnemyDied;

        public void Init (PlayerComponent player) 
        {
            _player = player;
            SwitchState(_startState);

            _enemy.EnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            _currentState.StateFinished -= SwitchState;
            _enemy.EnemyDied -= OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            EnemyDied?.Invoke(this);
            Destroy(gameObject);
        }

        private void SwitchState(State state)
        {
            _currentState = state;
            state.Enter(_player);

            _currentState.StateFinished += SwitchState;
        }

    }
}
