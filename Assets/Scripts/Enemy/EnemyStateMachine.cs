using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _startState;

        private Player _player;
        private State _currentState;

        public void Init (Player player) 
        {
            _player = player;
            SwitchState(_startState);
        }

        private void OnDisable()
        {
            _currentState.StateFinished -= SwitchState;
        }

        private void SwitchState(State state)
        {
            _currentState = state;
            state.Enter(_player);

            _currentState.StateFinished += SwitchState;
        }

    }
}
