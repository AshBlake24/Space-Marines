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
        public void SetOneHandedWeapon() => _animator.SetBool(s_hasOneHanded, true);
        public void SetTwoHandedWeapon() => _animator.SetBool(s_hasTwoHanded, true);

        public void RemoveWeapon()
        {
            _animator.SetBool(s_hasOneHanded, false);
            _animator.SetBool(s_hasTwoHanded, false);
        }
    }
}