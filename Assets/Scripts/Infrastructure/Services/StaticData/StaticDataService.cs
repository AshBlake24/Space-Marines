using System.Collections.Generic;
using System.Linq;
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

        public PlayerStaticData Player { get; private set; }

        public void LoadPlayer()
        {
            Player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerStaticData");
        }

        public void LoadWeapons()
        {
            _weapons = Resources.LoadAll<WeaponStaticData>("StaticData/Weapons")
                .ToDictionary(weapon => weapon.Id);
        }

        public void LoadProjectiles()
        {
            _projectiles = Resources.LoadAll<ProjectileStaticData>("StaticData/Projectiles")
                .ToDictionary(projectile => projectile.Id);
        }

        public WeaponStaticData GetWeaponData(WeaponId id) =>
            _weapons.TryGetValue(id, out WeaponStaticData staticData)
                ? staticData
                : null;

        public ProjectileStaticData GetProjectileData(ProjectileId id) =>
            _projectiles.TryGetValue(id, out ProjectileStaticData staticData)
                ? staticData
                : null;
    }
}