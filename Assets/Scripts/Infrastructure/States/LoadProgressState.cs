using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;
using Roguelike.Weapons;

namespace Roguelike.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentDataService progressService,
            ISaveLoadService saveLoadService, IWeaponFactory weaponFactory, IStaticDataService staticDataService)
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

            _stateMachine.Enter<LoadLevelState, string>(
                _progressService.PlayerProgress.WorldData.CurrentLevel.ToString());
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
            PlayerProgress playerProgress = new(
                _staticDataService.GameConfig.StartLevel,
                _staticDataService.GameConfig.StartStage,
                CreateStartWeapons(),
                _staticDataService.Player.StartCharacter.Id);

            playerProgress.State.MaxHealth = _staticDataService.Player.StartCharacter.MaxHealth;
            playerProgress.State.ResetHealth();

            return playerProgress;
        }

        private IEnumerable<IWeapon> CreateStartWeapons() =>
            _staticDataService.Player.StartWeapons
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId))
                .ToList();
    }
}