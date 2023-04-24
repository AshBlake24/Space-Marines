using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.Player.Skills;
using Roguelike.StaticData.Characters;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Roguelike.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWeaponFactory _weaponFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentDataService _persistentData;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(
            IAssetProvider assetProvider, 
            IPersistentDataService persistentData,
            IStaticDataService staticDataService, 
            ISaveLoadService saveLoadService, 
            IWeaponFactory weaponFactory,
            ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _weaponFactory = weaponFactory;
            _coroutineRunner = coroutineRunner;
            _persistentData = persistentData;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public GameObject CreatePlayer(Transform playerInitialPoint)
        {
            GameObject player = InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);
            GameObject character = CreateCharacter(_persistentData.PlayerProgress.Character, player);

            InitializeShooterComponent(player, character.GetComponentInChildren<WeaponSpawnPoint>().transform);

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Construct(_staticDataService.Player.ImmuneTimeAfterHit);

            ISkill skill = new RegenerationSkill(
                _coroutineRunner,
                playerHealth,
                healthPerTick: 1,
                ticksCount: 3,
                cooldownBetweenTicks: 3,
                skillCooldown: 60);
            
            
            PlayerSkill playerSkill = player.GetComponent<PlayerSkill>();
            playerSkill.Construct(skill);

            return player;
        }

        public GameObject CreateDesktopHud() =>
            InstantiateRegistered(AssetPath.DesktopHudPath);

        public GameObject CreateMobileHud() =>
            InstantiateRegistered(AssetPath.MobileHudPath);

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

        private void InitializeShooterComponent(GameObject player, Transform weaponSpawnPoint)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();

            List<IWeapon> weapons = _persistentData.PlayerProgress.PlayerWeapons.GetWeapons()
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId, weaponSpawnPoint))
                .ToList();

            playerShooter.Construct(weapons, _staticDataService.Player.WeaponSwtichCooldown, weaponSpawnPoint);
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
    }
}