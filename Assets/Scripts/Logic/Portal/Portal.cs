using Roguelike.Data;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.Logic.Portal
{
    [RequireComponent(typeof(Collider))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _portalVFX;
        [SerializeField] private AudioSource _audio;

        private IPersistentDataService _progressService;
        private ISceneLoadingService _sceneLoadingService;
        private LevelId _levelId;
        private RegionId _regionId;
        private StageId _startStageId;

        private void Awake()
        {
            _collider.enabled = false;
            _progressService = AllServices.Container.Single<IPersistentDataService>();
            _sceneLoadingService = AllServices.Container.Single<ISceneLoadingService>();
        }

        public void Init(LevelId levelId, RegionId regionId, StageId startStageId)
        {
            _levelId = levelId;
            _regionId = regionId;
            _startStageId = startStageId;
            _collider.enabled = true;
            _audio.Play();
            _portalVFX.Play();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                _progressService.PlayerProgress.WorldData = new WorldData(_levelId, _regionId, _startStageId);
                _sceneLoadingService.Load(_levelId);
            }
        }
    }
}