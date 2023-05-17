using System;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int s_speed = Animator.StringToHash("Speed");
        private static readonly int s_dieHash = Animator.StringToHash("Die");
        private static readonly int s_shot = Animator.StringToHash("Shot");
        private static readonly int s_hit = Animator.StringToHash("Hit");
        private static readonly int s_hasOneHanded = Animator.StringToHash("Has1HWeapon");
        private static readonly int s_hasTwoHanded = Animator.StringToHash("Has2HWeapon");
        
        private Animator _animator;

        public event Action Restarted;

        public void Construct(Animator animator) => 
            _animator = animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector3 velocity)
        {
            _animator.SetFloat(s_speed, velocity.magnitude);
        }

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
                case WeaponSize.Unknown:
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