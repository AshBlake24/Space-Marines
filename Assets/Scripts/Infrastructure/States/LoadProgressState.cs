using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Weapons;

namespace Roguelike.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string StartLevel = "Hub";
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadService saveLoadService, IWeaponFactory weaponFactory, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _weaponFactory = weaponFactory;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgress();
            
            _stateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.CurrentLevel);
        }

        public void Exit()
        {
        }

        private void LoadProgress() => 
            _progressService.PlayerProgress =
                _saveLoadService.LoadProgress()
                ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(StartLevel, CreateStartWeapons());

            playerProgress.State.MaxHealth = 3;
            playerProgress.State.ResetHealth();

            return playerProgress;
        }

        private IEnumerable<IWeapon> CreateStartWeapons() => 
            _staticDataService.Player.StartWeapons
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId))
                .ToList();
    }
}