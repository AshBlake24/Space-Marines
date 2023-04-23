using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponId, WeaponStaticData> _weapons;
        private Dictionary<ProjectileId, ProjectileStaticData> _projectiles;
        private Dictionary<CharacterId, CharacterStaticData> _characters;

        public PlayerStaticData Player { get; private set; }

        public void Load()
        {
            LoadWeapons();
            LoadProjectiles();
            LoadCharacters();
            LoadPlayer();;
        }

        public WeaponStaticData GetWeaponData(WeaponId id) =>
            _weapons.TryGetValue(id, out WeaponStaticData staticData)
                ? staticData
                : null;

        public ProjectileStaticData GetProjectileData(ProjectileId id) =>
            _projectiles.TryGetValue(id, out ProjectileStaticData staticData)
                ? staticData
                : null;

        private void LoadWeapons() =>
            _weapons = Resources.LoadAll<WeaponStaticData>("StaticData/Weapons")
                .ToDictionary(weapon => weapon.Id);

        private void LoadProjectiles() =>
            _projectiles = Resources.LoadAll<ProjectileStaticData>("StaticData/Projectiles")
                .ToDictionary(projectile => projectile.Id);

        private void LoadCharacters() =>
            _characters = Resources.LoadAll<CharacterStaticData>("StaticData/Characters")
                .ToDictionary(character => character.Id);

        private void LoadPlayer() => 
            Player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerStaticData");
    }
}