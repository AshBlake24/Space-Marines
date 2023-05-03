using Roguelike.Data;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
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
        
        private ISaveLoadService _saveLoadService;
        private IPersistentDataService _progressService;
        private ISceneLoadingService _sceneLoadingService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
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
                _saveLoadService.SaveProgress();
                _sceneLoadingService.Load(DungeonId);
            }
        }
    }
}