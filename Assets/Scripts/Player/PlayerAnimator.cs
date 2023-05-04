using System;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int s_speedHash = Animator.StringToHash("Speed");
        private static readonly int s_dieHash = Animator.StringToHash("Die");
        private static readonly int s_shot = Animator.StringToHash("Shot");
        private static readonly int s_hit = Animator.StringToHash("Hit");
        private static readonly int s_hasOneHanded = Animator.StringToHash("HasOneHandedWeapon");
        private static readonly int s_hasTwoHanded = Animator.StringToHash("HasTwoHandedWeapon");
        
        private Animator _animator;

        public event Action Restarted;

        public void Construct(Animator animator) => 
            _animator = animator;

        public void Move(float speed) =>
            _animator.SetFloat(s_speedHash, speed);

        public void PlayHit() => _animator.SetTrigger(s_hit);

        public void PlayShot() => _animator.SetTrigger(s_shot);

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

        public void Restart()
        {
            _animator.Rebind();
            _animator.Update(0f);
            Restarted?.Invoke();
        }
    }
}