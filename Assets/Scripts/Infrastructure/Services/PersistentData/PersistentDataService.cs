using Roguelike.Data;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.States;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public class PersistentDataService : IPersistentDataService
    {
        private readonly IStaticDataService _staticData;
        private readonly GameStateMachine _stateMachine;
        
        public bool IsResetting { get; private set; }

        public PersistentDataService(IStaticDataService staticData, GameStateMachine stateMachine)
        {
            _staticData = staticData;
            _stateMachine = stateMachine;
            IsResetting = false;
        }

        public PlayerProgress PlayerProgress { get; set; }

        public void Reset()
        {
            PlayerProgress.State.Reset();
            PlayerProgress.Balance.Reset();
            PlayerProgress.KillData.CurrentKillData.Reset();
            PlayerProgress.WorldData = new WorldData(
                _staticData.GameConfig.StartPlayerLevel,
                _staticData.GameConfig.StartPlayerStage);
        }

        public void ResetAllProgress()
        {
            IsResetting = true;
            PlayerPrefs.DeleteAll();
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}