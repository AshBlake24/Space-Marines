using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        
        private Transform _weaponSpawnPoint;
        private IInputService _inputService;
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        public void Construct(Transform weaponSpawnPoint, List<IWeapon> weapons)
        {
            _weaponSpawnPoint = weaponSpawnPoint;
            _weapons = weapons;
        }
    }
}