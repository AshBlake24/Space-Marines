using Cinemachine;
using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Assets.Scripts.Enemies
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private EnemyId _boosId;

        private Room _room;
        private PlayerHealth _player;
        private EnterTriger _enterPoint;
        private IEnemyFactory _enemyFactory;

        public void Init(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _room = GetComponent<Room>();
            _enterPoint = _room.EntryPoint.GetComponent<EnterTriger>();

            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            _player = player;

            _camera = FindObjectOfType<CinemachineVirtualCamera>();

            Transform transform = Spawn(_spawnPoint, _player);

            LookAtBoss(transform);
        }

        private Transform Spawn(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemyPrefab = _enemyFactory.CreateEnemy(spawnPosition, _boosId, target);

            enemyPrefab.GetComponent<EnemyStateMachine>().enabled = true;

            return enemyPrefab.transform;
        }

        private void LookAtBoss(Transform character)
        {
            _camera.Follow = character;
        }
    }
}