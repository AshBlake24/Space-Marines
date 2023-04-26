using UnityEngine;
using Roguelike.Enemies.EnemyStates;

namespace Roguelike.Enemies
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyState _startState;

        private EnemyHealth _enemyHealth;
        private Enemy _enemy;
        private EnemyState _currentState;

        public Enemy Enemy => _enemy;

        public void Init (Enemy enemy) 
        {
            _enemy = enemy;
            _enemyHealth = _enemy.Health;
            SwitchState(_startState);

            _enemyHealth.Died += OnEnemyDead;
        }

        private void OnDisable()
        {
            _currentState.StateFinished -= SwitchState;
        }

        private void SwitchState(EnemyState state)
        {
            _currentState = state;
            state.Enter(_enemy);

            _currentState.StateFinished += SwitchState;
        }

        private void OnEnemyDead(EnemyHealth enemy)
        {
            enemy.Died -= OnEnemyDead;
            Destroy(gameObject);
        }
    }
}
