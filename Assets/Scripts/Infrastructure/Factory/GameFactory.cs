using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Environment;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Characters;
using Roguelike.UI.Elements;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.StaticData.Levels;
using System;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWeaponFactory _weaponFactory;
        private readonly ISkillFactory _skillFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentDataService _persistentData;
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;
        private readonly IEnemyFactory _enemyFactory;

        public GameFactory(IAssetProvider assetProvider,
            IPersistentDataService persistentData,
            IStaticDataService staticDataService,
            ISaveLoadService saveLoadService,
            IWeaponFactory weaponFactory,            
            ISkillFactory skillFactory,
            IEnemyFactory enemyFactory,
            IWindowService windowService)
        {
            _assetProvider = assetProvider;
            _persistentData = persistentData;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _weaponFactory = weaponFactory;
            _skillFactory = skillFactory;
            _windowService = windowService;
            _enemyFactory = enemyFactory;
        }

        public GameObject CreatePlayer(Transform playerInitialPoint)
        {
            GameObject player = InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);
            GameObject character = CreateCharacter(_persistentData.PlayerProgress.Character, player);

            InitializeShooterComponent(player, character.GetComponentInChildren<WeaponSpawnPoint>());

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Construct(_staticDataService.Player.ImmuneTimeAfterHit);
            
            _skillFactory.CreatePlayerSkill(player);
            
            return player;
        }

        public GameObject CreateHud(EnvironmentType deviceType)
        {
            GameObject hud = InstantiateRegistered(deviceType == EnvironmentType.Desktop
                ? AssetPath.DesktopHudPath
                : AssetPath.MobileHudPath);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);

            return hud;
        }

        private GameObject CreateCharacter(CharacterId id, GameObject player)
        {
            CharacterStaticData characterData = _staticDataService.GetCharacterData(id);
            GameObject character = Object.Instantiate(
                characterData.Prefab, 
                player.transform.position, 
                Quaternion.identity, 
                player.transform);
            
            player.GetComponent<PlayerAnimator>()
                .Construct(character.GetComponent<Animator>());
            
            MultiAimConstraint multiAimConstraint = player.GetComponentInChildren<MultiAimConstraint>();
            multiAimConstraint.data.sourceObjects = new WeightedTransformArray()
            {
                new(player.GetComponentInChildren<AimTarget>().transform, 1)
            };
                

            character.GetComponentInChildren<RigBuilder>().Build();

            return character;
        }

        private void InitializeShooterComponent(GameObject player, WeaponSpawnPoint weaponSpawnPoint)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();

            List<IWeapon> weapons = _persistentData.PlayerProgress.PlayerWeapons.GetWeapons()
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId, weaponSpawnPoint.transform))
                .ToList();

            playerShooter.Construct(
                weapons, 
                _staticDataService.Player.WeaponSwtichCooldown, 
                weaponSpawnPoint,
                _weaponFactory);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);
            _saveLoadService.RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 postition)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, postition);
            _saveLoadService.RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        public GameObject GenerateLevel()
        {
            StageId id = _persistentData.PlayerProgress.WorldData.CurrentStage;

            LevelStaticData levelData = _staticDataService.GetLevelStaticData(id);

            GameObject LevelGeneratorPrefab = InstantiateRegistered(AssetPath.LevelGeneratorPath);

            LevelGenerator levelGenerator = LevelGeneratorPrefab.GetComponent<LevelGenerator>();

            levelGenerator.Init(levelData);
            levelGenerator.BuildLevel(_enemyFactory);

            return LevelGeneratorPrefab;
        }

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }
    }
}