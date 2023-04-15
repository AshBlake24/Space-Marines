using System.Collections.Generic;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.StaticData.Weapons;
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

        public LoadProgressState(GameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadService saveLoadService, IWeaponFactory weaponFactory)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _weaponFactory = weaponFactory;
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
            IWeapon weapon = _weaponFactory.CreateWeapon(WeaponId.BasicPistol);
            IEnumerable<IWeapon> weapons = new []
            {
                weapon
            };


            return new PlayerProgress(StartLevel, weapons);
        }
    }
}