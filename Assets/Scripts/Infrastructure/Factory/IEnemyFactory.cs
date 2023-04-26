using Roguelike.Infrastructure.Services;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        public GameObject CreateEnemy(Transform spawnPiont, EnemyId id, PlayerHealth target);
    }
}
