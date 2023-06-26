using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using Roguelike.UI.Elements;
using System;
using Roguelike.Infrastructure.Services.PersistentData;
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
        private IPersistentDataService _persistentData;

        public event Action BossDied; 

        private void OnDisable()
        {
            if (_boss != null)
                _boss.Died -= OnBossDied;
        }

        public void Init(IEnemyFactory enemyFactory, IPersistentDataService persistentDataService)
        {
            _enemyFactory = enemyFactory;
            _persistentData = persistentDataService;

            _room = GetComponent<Room>();
            _enterPoint = _room.EntryPoint.GetComponent<EnterTriger>();

            if(_finishLevelzone != null)
                _finishLevelzone.SetActive(false);

            Quaternion rotation = Quaternion.Euler(0, _room.EntryPoint.transform.rotation.eulerAngles.y, 0);
            _spawnPoint.rotation = rotation;
            _finishLevelzone.transform.rotation = Quaternion.identity;

            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            Spawn(_spawnPoint, player);
        }

        private Transform Spawn(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemyPrefab = _enemyFactory.CreateBoss(spawnPosition, _boosId, target);

            _boss = enemyPrefab.GetComponentInChildren<EnemyHealth>();
            _boss.Died += OnBossDied;

            _room.CloseDoor();

            return enemyPrefab.transform;
        }

        private void OnBossDied(EnemyHealth enemy)
        {
            _finishLevelzone.SetActive(true);
            _boss.Died -= OnBossDied;

            _room.OpenDoor();
            
            int coins = enemy.GetComponentInParent<BossRoot>()
                .GetComponentInChildren<BossStateMachine>().Enemy.Coins;
            
            _persistentData.PlayerProgress.Statistics.KillData.CurrentKillData.OnBossKilled();
            _persistentData.PlayerProgress.Statistics.KillData.CurrentKillData.OnMonsterKilled();
            _persistentData.PlayerProgress.Balance.AddCoins(coins);

            BossDied?.Invoke();
        }
    }
}