using UnityEngine;

namespace Roguelike.Enemies
{
    public class BossStage : MonoBehaviour
    {
        [SerializeField, Range(1f, 100f)] private float _healthPersentForNextStage;
        [SerializeField] private EnemyStateMachine _firstStage;
        [SerializeField] private EnemyStateMachine _nextStage;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private Transform _cameraPoint;

        private Enemy _enemy;

        private void OnDisable()
        {
            if (_enemy != null)
                _enemy.Health.HealthChanged += OnHealthChanged;
        }

        public void Init(Enemy enemy)
        {
            _enemy= enemy;

            _firstStage.gameObject.SetActive(true);
            _enemyHealth.transform.SetParent(_firstStage.transform);
            _cameraPoint.transform.SetParent(_firstStage.transform);

            _firstStage.Init(_enemy);

            _firstStage.enabled = true;

            _nextStage.gameObject.SetActive(false);
            _nextStage.enabled = false;

            _firstStage.Init(_enemy);

            _enemy.Health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_enemy.Health.CurrentHealth < (_enemy.Health.MaxHealth * _healthPersentForNextStage / 100))
            {
                _firstStage.gameObject.SetActive(false);
                _firstStage.enabled = false;

                _nextStage.gameObject.SetActive(true);
                _enemyHealth.transform.SetParent(_nextStage.transform);
                _cameraPoint.transform.SetParent(_nextStage.transform);
                _enemyHealth.transform.localPosition = Vector3.zero;

                _nextStage.Init(_enemy);

                _nextStage.enabled = true;

                _enemy.Health.HealthChanged -= OnHealthChanged;
            }
        }
    }
}