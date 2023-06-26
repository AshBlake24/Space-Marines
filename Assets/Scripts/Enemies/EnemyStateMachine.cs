using UnityEngine;
using Roguelike.Enemies.EnemyStates;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.Player;

namespace Roguelike.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]

    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyState _startState;
        [SerializeField] private EnemyState _dieState;
        [SerializeField] private EnemyState _playerDieState;

        private EnemyHealth _enemyHealth;
        private Enemy _enemy;
        private EnemyState _currentState;
        private EnemyAnimator _animator;
        private PlayerDeath _playerDeath;

        public Enemy Enemy => _enemy;

        private void OnEnable()
        {
            SwitchState(_startState);
        }

        public virtual void Init (Enemy enemy) 
        {
            _enemy = enemy;
            _enemyHealth = _enemy.Health;

            _playerDeath = _enemy.Target.GetComponent<PlayerDeath>();
            _animator = GetComponent<EnemyAnimator>();

            _enemyHealth.OnDied += OnEnemyDead;
            _playerDeath.Died += OnPlayerDead;
        }

        private void OnDisable()
        {
            _currentState.StateFinished -= SwitchState;
            _playerDeath.Died -= OnPlayerDead;
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
            enemy.OnDied -= OnEnemyDead;

            _currentState.Exit(_dieState);
        }

        private void OnPlayerDead()
        {
            if (_enemy.Health.CurrentHealth > 0)
                _currentState.Exit(_playerDieState);
        }
    }
}
