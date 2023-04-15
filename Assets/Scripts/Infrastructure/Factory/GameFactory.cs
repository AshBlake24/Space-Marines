using System.Collections.Generic;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
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
        
        public GameFactory(IAssetProvider assetProvider, IPersistentDataService persistentData, ISaveLoadService saveLoadService, IWeaponFactory weaponFactory)
        {
            _assetProvider = assetProvider;
            _weaponFactory = weaponFactory;
            _persistentData = persistentData;
            _saveLoadService = saveLoadService;
        }
        
        public GameObject CreatePlayer(Transform playerInitialPoint)
        {
            GameObject player = InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);

            List<IWeapon> weapons = new();
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();

            foreach (RangedWeaponsData rangedWeapon in _persistentData.PlayerProgress.PlayerWeapons.RangedWeapons)
            {
                weapons.Add(_weaponFactory.CreateWeapon(rangedWeapon.ID, playerShooter.WeaponSpawnPoint));
            }
            
            playerShooter.Construct(weapons);

            return player;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 postition)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, postition);
            _saveLoadService.RegisterProgressWatchers(gameObject);

            return gameObject;
        }
    }
}