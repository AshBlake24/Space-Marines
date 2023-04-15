using System;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int s_speedHash = Animator.StringToHash("Speed");
        private static readonly int s_dieHash = Animator.StringToHash("Die");
        private static readonly int s_hasOneHanded = Animator.StringToHash("HasOneHandedWeapon");
        private static readonly int s_hasTwoHanded = Animator.StringToHash("HasTwoHandedWeapon");

        [SerializeField] private Animator _animator;

        public void Move(float speed) =>
            _animator.SetFloat(s_speedHash, speed);

        public void PlayDeath() => _animator.SetTrigger(s_dieHash);

        public void SetWeapon(WeaponSize weaponSize)
        {
            switch (weaponSize)
            {
                case WeaponSize.OneHanded:
                    SetWeapon(true, false);
                    break;
                case WeaponSize.TwoHanded:
                    SetWeapon(false, true);
                    break;
                default:
                    SetWeapon(false, false);
                    break;
            }
        }

        private void SetWeapon(bool hasOneHanded, bool hasTwoHanded)
        {
            _animator.SetBool(s_hasOneHanded, hasOneHanded);
            _animator.SetBool(s_hasTwoHanded, hasTwoHanded);
        }
    }
}