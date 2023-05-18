using System;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Environment;
using Roguelike.Infrastructure.Services.Loading;
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
using Roguelike.Level;
using Roguelike.StaticData.Levels;
using Object = UnityEngine.Object;
using Roguelike.Logic.CharacterSelection;
using Roguelike.StaticData.Skills;
using Roguelike.UI.Buttons;
using Roguelike.UI.Windows;
using Roguelike.Weapons.Logic;

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
        private readonly IEnemyFactory _enemyFactory;
        private readonly IWindowService _windowService;
        private readonly IEnvironmentService _environmentService;
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly ILootFactory _lootFactory;

        public GameFactory(IAssetProvider assetProvider,
            IPersistentDataService persistentData,
            IStaticDataService staticDataService,
            ISaveLoadService saveLoadService,
            IWeaponFactory weaponFactory,
            ISkillFactory skillFactory,
            IEnemyFactory enemyFactory,
            IWindowService windowService,
            IEnvironmentService environmentService,
            ISceneLoadingService sceneLoadingService,
            ILootFactory lootFactory)
        {
            _assetProvider = assetProvider;
            _persistentData = persistentData;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _weaponFactory = weaponFactory;
            _skillFactory = skillFactory;
            _windowService = windowService;
            _environmentService = environmentService;
            _sceneLoadingService = sceneLoadingService;
            _lootFactory = lootFactory;
            _enemyFactory = enemyFactory;
        }

        public GameObject CreatePlayer(Transform playerInitialPoint)
        {
            GameObject player = _assetProvider.InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);
            GameObject character = CreateCharacter(_persistentData.PlayerProgress.Character, player);

            InitShooterComponent(player, character.GetComponentInChildren<WeaponSpawnPoint>());

            player.GetComponent<PlayerHealth>()
                .Construct(_staticDataService.Player.ImmuneTimeAfterHit,
                    _staticDataService.Player.ImmuneTimeAfterResurrect);

            player.GetComponent<PlayerDeath>()
                .Construct(_windowService, _saveLoadService);
            
            player.GetComponent<PlayerInteraction>()
                .Construct(_windowService);

            _skillFactory.CreatePlayerSkill(player);

            return player;
        }

        public GameObject CreateHud(GameObject player, bool createMiniMap)
        {
            EnvironmentType deviceType = _environmentService.GetDeviceType();

            GameObject hud = _assetProvider.InstantiateRegistered(deviceType == EnvironmentType.Desktop
                ? AssetPath.DesktopHudPath
                : AssetPath.MobileHudPath);

            if (createMiniMap)
                Object.Instantiate(Resources.Load(AssetPath.MiniMap), hud.transform);

            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            CharacterStaticData characterData = _staticDataService
                .GetCharacterData(_persistentData.PlayerProgress.Character);
            SkillStaticData skillData = _staticDataService
                .GetSkillData(characterData.Skill);

            hud.GetComponentInChildren<PlayerWeaponsViewer>()
                .Construct(playerShooter);

            hud.GetComponentInChildren<AmmoCounter>()
                .Construct(playerShooter);

            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<PlayerHealth>());

            hud.GetComponentInChildren<CharacterIcon>()
                .Construct(characterData.Icon);

            hud.GetComponentInChildren<ActiveSkillObserver>()
                .Construct(player.GetComponent<PlayerSkill>(), skillData.Icon);

            MobileActionButton mobileActionButton = hud.GetComponentInChildren<MobileActionButton>();

            if (mobileActionButton != null)
                mobileActionButton.Construct(player.GetComponent<PlayerInteraction>());

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);

            return hud;
        }
        
        public GameObject GenerateLevel()
        {
            StageId id = _persistentData.PlayerProgress.WorldData.CurrentStage;

            GameObject LevelGeneratorPrefab = _assetProvider.InstantiateRegistered(AssetPath.LevelGeneratorPath);
            LevelGenerator levelGenerator = LevelGeneratorPrefab.GetComponent<LevelGenerator>();
            LevelStaticData levelData = _staticDataService.GetLevelData(id);

            levelGenerator.Construct(levelData, _persistentData, _sceneLoadingService, _enemyFactory);
            levelGenerator.BuildLevel();

            return LevelGeneratorPrefab;
        }

        public void CreateCharacterSelectionMode()
        {
            BaseWindow characterSelectionWindow = _windowService.Open(WindowId.CharacterSelection);

            if (Camera.main.TryGetComponent(out CharacterSelectionMode characterSelection))
                characterSelection.Construct(this, _staticDataService, _windowService, _saveLoadService, _weaponFactory,
                    characterSelectionWindow);
            else
                throw new ArgumentNullException(nameof(Camera),
                    "Camera is missing a component of CharacterSelectionMode");
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

            InitIK(player, character);
            
            character.GetComponent<CharacterIKAiminig>()
                .Construct(player.GetComponent<PlayerShooter>());

            return character;
        }

        private static void InitIK(GameObject player, GameObject character)
        {
            MultiAimConstraint[] multiAimConstraint = player.GetComponentsInChildren<MultiAimConstraint>();

            foreach (MultiAimConstraint constraint in multiAimConstraint)
            {
                constraint.data.sourceObjects = new WeightedTransformArray
                {
                    new(player.GetComponentInChildren<AimTarget>().transform, 1)
                };
            }

            RigBuilder[] rigBuilders = character.GetComponentsInChildren<RigBuilder>();

            foreach (RigBuilder rigBuilder in rigBuilders)
                rigBuilder.Build();
        }

        private void InitShooterComponent(GameObject player, WeaponSpawnPoint weaponSpawnPoint)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            
            player.AddComponent<WeaponsObserver>()
                .Construct(_persistentData, playerShooter);

            IWeapon[] weapons = _persistentData.PlayerProgress.PlayerWeapons.Weapons
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId, weaponSpawnPoint.transform))
                .ToArray();

            playerShooter.Construct(
                weapons,
                _weaponFactory,
                _lootFactory,
                _staticDataService.Player.WeaponSwtichCooldown,
                weaponSpawnPoint);
        }
    }
}