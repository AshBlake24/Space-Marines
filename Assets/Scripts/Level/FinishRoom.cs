using System;
using Roguelike.Assets.Scripts.Enemies;
using Roguelike.Data;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.Level
{
    public class FinishRoom : Room, IProgressWriter
    {
        private const LevelId hub = LevelId.Hub;

        [SerializeField] private EnterTriger _enterTriger;

        private StageId _nextStageId;
        private IPersistentDataService _persistentDataService;
        private IWindowService _windowService;
        private bool _isBossRoom;

        public event Action PlayerFinishedLevel;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
        }

        private void OnEnable()
        {
            _enterTriger.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnDisable()
        {
            _enterTriger.PlayerHasEntered -= OnPlayerHasEntered;
        }

        public void SetNextLevel(StageId nextStageId, IPersistentDataService persistentDataService)
        {
            _nextStageId = nextStageId;
            _persistentDataService = persistentDataService;

            if (TryGetComponent<BossSpawner>(out BossSpawner spawner))
            {
                _isBossRoom = true;
            }
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            if (_isBossRoom)
            {
                _windowService.Open(WindowId.GameComplete);
                return;
            }
            
            WriteProgress(_persistentDataService.PlayerProgress);
            PlayerFinishedLevel?.Invoke();
        }

        public void WriteProgress(PlayerProgress progress)
        {
            progress.WorldData.CurrentStage = _nextStageId;
        }

        public void ReadProgress(PlayerProgress progress)
        {
        }
    }
}
