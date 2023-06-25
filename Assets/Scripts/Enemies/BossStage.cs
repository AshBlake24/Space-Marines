using UnityEngine;
using UnityEngine.UIElements;

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

            SwitchStage(_nextStage, _firstStage);

            _enemy.Health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_enemy.Health.CurrentHealth < (_enemy.Health.MaxHealth * _healthPersentForNextStage / 100))
            {
                SwitchStage(_firstStage, _nextStage);
                //_firstStage.gameObject.SetActive(false);
                //_firstStage.enabled = false;

                //_nextStage.gameObject.SetActive(true);
                //_nextStage.transform.position = _firstStage.transform.position;
                //_enemyHealth.transform.SetParent(_nextStage.transform);

                //Vector3 position = _cameraPoint.localPosition;
                //_cameraPoint.transform.SetParent(_nextStage.transform);
                //_cameraPoint.transform.localPosition = new Vector3(0, position.y, 0);
                //_cameraPoint.transform.localRotation = Quaternion.identity;

                //_enemyHealth.transform.localPosition = Vector3.zero;

                //_nextStage.Init(_enemy);

                //_nextStage.enabled = true;

                _enemy.Health.HealthChanged -= OnHealthChanged;
            }
        }

        private void SwitchStage(EnemyStateMachine previouslyStage, EnemyStateMachine nextStage)
        {
            nextStage.enabled = false;
            previouslyStage.gameObject.SetActive(false);

            nextStage.transform.position = previouslyStage.transform.position;

            _enemyHealth.transform.SetParent(nextStage.transform);
            _enemyHealth.transform.localPosition = Vector3.zero;

            Vector3 position = _cameraPoint.localPosition;
            _cameraPoint.transform.SetParent(nextStage.transform);
            _cameraPoint.transform.localPosition = new Vector3(0, position.y, 0);
            _cameraPoint.transform.localRotation = Quaternion.identity;

            nextStage.gameObject.SetActive(true);

            _nextStage.Init(_enemy);

            nextStage.enabled = true;
        }
    }
}