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

        public void Construct(PlayerShooter playerShooter)
        {
            _playerShooter = playerShooter;
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }
        
        private void OnDestroy() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void Start()
        {
            if (_aimWith1H == null && _aimWith2H == null)
                enabled = false;
        }

        private void OnWeaponChanged()
        {
            if (_playerShooter.CurrentWeapon == null)
            {
                SetWeightToConstraint(_aimWith1H, _disabledAimWeight);
                SetWeightToConstraint(_aimWith2H, _disabledAimWeight);
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