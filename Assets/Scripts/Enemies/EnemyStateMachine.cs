using UnityEngine;
using Roguelike.Enemies.EnemyStates;
using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]

    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyState _startState;
        [SerializeField] private EnemyState _dieState;

        private EnemyHealth _enemyHealth;
        private Enemy _enemy;
        private EnemyState _currentState;
        private EnemyAnimator _animator;

        public Enemy Enemy => _enemy;

        private void OnEnable()
        {
            SwitchState(_startState);
        }

        public virtual void Init (Enemy enemy) 
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

            _currentState.StateFinished += SwitchState;

            state.Enter(_enemy, _animator);
        }

        private void OnEnemyDead(EnemyHealth enemy)
        {
            enemy.Died -= OnEnemyDead;

            _currentState.Exit(_dieState);
        }
    }
}
