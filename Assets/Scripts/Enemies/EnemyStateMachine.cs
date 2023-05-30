using UnityEngine;
using Roguelike.Enemies.EnemyStates;
using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]

    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyState _startState;

        private EnemyHealth _enemyHealth;
        private Enemy _enemy;
        private EnemyState _currentState;
        private EnemyAnimator _animator;

        public Enemy Enemy => _enemy;

        private void OnEnable()
        {
            SwitchState(_startState);
        }

        public void Init (Enemy enemy) 
        {
            _enemy = enemy;
            _enemyHealth = _enemy.Health;

            _animator = GetComponent<EnemyAnimator>();

            _enemyHealth.Died += OnEnemyDead;
        }

        private void OnDisable()
        {
            _currentState.StateFinished -= SwitchState;
        }

        private void SwitchState(EnemyState state)
        {
            if (_currentState != null)
            {
                _currentState.StateFinished -= SwitchState;
                _currentState.enabled = false;
            }

            _currentState = state;
            state.Enter(_enemy, _animator);

            _currentState.StateFinished += SwitchState;
        }

        private void OnEnemyDead(EnemyHealth enemy)
        {
            enemy.Died -= OnEnemyDead;
            Destroy(gameObject);
        }
    }
}
