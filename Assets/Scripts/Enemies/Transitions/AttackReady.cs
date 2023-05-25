using UnityEngine;

namespace Roguelike.Enemies.Transitions
{
    public class AttackReady : Transition
    {
        private Enemy _enemy;
        private float _accumulatedTime;

        private void OnEnable()
        {
            _enemy = GetComponent<EnemyStateMachine>().Enemy;

            _accumulatedTime = 0;
        }

        private void Update()
        {
            if (_enemy != null)
            {
                _accumulatedTime += Time.deltaTime;

                if (_accumulatedTime >= _enemy.AttackColldown)
                    NeedTransit?.Invoke(targetState);
            }
        }
    }
}