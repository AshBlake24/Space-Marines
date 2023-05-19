using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _spawnEffect;

        private GameObject _enemy;
        private float _currentTime;
        private bool _isReady;

        public event Action<SpawnPoint, GameObject> EnemySpawned;

        private void Update()
        {
            if (_enemy == null || _isReady)
                return;

            if (_currentTime > 0)
                _currentTime -= Time.deltaTime;
            else
                ActivateEnemy();
        }

        public void Spawn(GameObject enemy, float duration)
        {
            _isReady = false;
            _currentTime = duration;
            _enemy = enemy;

            _spawnEffect.Play();
        }

        private void ActivateEnemy()
        {
            _isReady = true;

            _spawnEffect.Stop();

            _enemy.SetActive(true);

            EnemySpawned?.Invoke(this, _enemy);
        }
    }
}
