using Roguelike.Data;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.Logic
{
    [RequireComponent(typeof(Collider))]
    public class EnterTheDungeon : MonoBehaviour
    {
        private const LevelId DungeonId = LevelId.Dungeon;
        private const StageId StartDungeonStage = StageId.Level11;
        
        private IPersistentDataService _progressService;
        private ISceneLoadingService _sceneLoadingService;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPersistentDataService>();
            _sceneLoadingService = AllServices.Container.Single<ISceneLoadingService>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                _progressService.PlayerProgress.WorldData = new WorldData(
                    DungeonId,
                    StartDungeonStage);
                
                _sceneLoadingService.Load(DungeonId);
            }
        }
    }
}