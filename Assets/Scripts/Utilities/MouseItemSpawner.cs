using System.Collections;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class MouseItemSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponId _weaponId;
        [SerializeField] private PowerupId _powerupId;

        private ILootFactory _lootFactory;
        private Camera _camera;
        private RaycastHit _hit;

        private void Awake()
        {
            _lootFactory = AllServices.Container.Single<ILootFactory>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _hit))
                    _lootFactory.CreateConcreteWeapon(_weaponId, _hit.point);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _hit))
                    _lootFactory.CreateConcretePowerup(_powerupId, _hit.point);
            }
        }
    }
}