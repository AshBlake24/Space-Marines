using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.UI;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWeaponFactory _weaponFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentDataService _persistentData;
        private readonly IStaticDataService _staticDataService;
        
        public GameFactory(IAssetProvider assetProvider, IPersistentDataService persistentData, IStaticDataService staticDataService, ISaveLoadService saveLoadService, IWeaponFactory weaponFactory)
        {
            _assetProvider = assetProvider;
            _weaponFactory = weaponFactory;
            _persistentData = persistentData;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }
        
        public GameObject CreatePlayer(Transform playerInitialPoint)
        {
            GameObject player = InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);
            
            InitializeShooterComponent(player);
            
            player.GetComponent<PlayerHealth>()
                .Construct(_staticDataService.Player.ImmuneTimeAfterHit);

            return player;
        }

        public GameObject CreateDesktopHud() => 
            InstantiateRegistered(AssetPath.DesktopHudPath);

        public GameObject CreateMobileHud()=>
            InstantiateRegistered(AssetPath.MobileHudPath);

        private void InitializeShooterComponent(GameObject player)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            
            List<IWeapon> weapons = _persistentData.PlayerProgress.PlayerWeapons.GetWeapons()
                .Select(weaponId => _weaponFactory.CreateWeapon(weaponId, playerShooter.WeaponSpawnPoint))
                .ToList();

            playerShooter.Construct(weapons, _staticDataService.Player.WeaponSwtichCooldown);
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