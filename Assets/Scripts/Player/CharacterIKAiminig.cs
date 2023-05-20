using System;
using System.Collections.Generic;
using Roguelike.StaticData.Weapons;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Roguelike.Player
{
    public class CharacterIKAiminig : MonoBehaviour
    {
        [SerializeField] private TwoBoneIKConstraint[] _aimWith1H;
        [SerializeField] private TwoBoneIKConstraint[] _aimWith2H;
        [SerializeField, Range(0f,1f)] private float _disabledAimWeight = 0f;
        [SerializeField, Range(0f,1f)] private float _enabledAimWeight = 1f;

        private PlayerShooter _playerShooter;
        private PlayerDeath _playerDeath;

        public void Construct(PlayerShooter playerShooter, PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;
            _playerShooter = playerShooter;
            _playerDeath.Died += OnDied;
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }
        private void OnDestroy()
        {
            _playerShooter.WeaponChanged -= OnWeaponChanged;
            _playerDeath.Died -= OnDied;
        }

        private void Start()
        {
            if (_aimWith1H == null && _aimWith2H == null)
                enabled = false;
        }

        private void OnDied() => DisableConstraints();

        private void DisableConstraints()
        {
            SetWeightToConstraint(_aimWith1H, _disabledAimWeight);
            SetWeightToConstraint(_aimWith2H, _disabledAimWeight);
        }

        private void OnWeaponChanged()
        {
            if (_playerShooter.CurrentWeapon == null)
            {
                DisableConstraints();
            }
            else switch (_playerShooter.CurrentWeapon.Stats.Size)
            {
                case WeaponSize.OneHanded:
                    SetWeightToConstraint(_aimWith1H, _enabledAimWeight);
                    SetWeightToConstraint(_aimWith2H, _disabledAimWeight);

                    break;
                case WeaponSize.TwoHanded:
                    SetWeightToConstraint(_aimWith1H, _disabledAimWeight);
                    SetWeightToConstraint(_aimWith2H, _enabledAimWeight);

                    break;
                case WeaponSize.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SetWeightToConstraint(IEnumerable<TwoBoneIKConstraint> constraints, float weight)
        {
            foreach (TwoBoneIKConstraint constraint in constraints)
                constraint.weight = weight;
        }
    }
}