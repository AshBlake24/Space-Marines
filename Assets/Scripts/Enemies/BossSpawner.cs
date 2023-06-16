using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using System;
using UnityEngine;

namespace Roguelike.Assets.Scripts.Enemies
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _finishLevelzone;
        [SerializeField] private EnemyId _boosId;

        private Room _room;
        private EnemyHealth _boss;
        private EnterTriger _enterPoint;
        private IEnemyFactory _enemyFactory;

        public event Action BossDied; 

        private void OnDisable()
        {
            if (_boss != null)
                _boss.Died -= OnBossDied;
        }

        public void Init(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _room = GetComponent<Room>();
            _enterPoint = _room.EntryPoint.GetComponent<EnterTriger>();

            if(_finishLevelzone.activeSelf)
                _finishLevelzone.SetActive(false);

            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            Spawn(_spawnPoint, player);
        }

        private Transform Spawn(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemyPrefab = _enemyFactory.CreateEnemy(spawnPosition, _boosId, target);

            enemyPrefab.GetComponent<EnemyStateMachine>().enabled = true;

            _boss = enemyPrefab.GetComponentInChildren<EnemyHealth>();
            _boss.Died += OnBossDied;

            return enemyPrefab.transform;
        }

        private void OnBossDied(EnemyHealth enemy)
        {
            _finishLevelzone.SetActive(true);
            _boss.Died -= OnBossDied;

            BossDied?.Invoke();
        }
    }
}