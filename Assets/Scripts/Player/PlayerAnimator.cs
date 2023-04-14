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
        public void SetOneHandedWeapon() => SetWeapon(true, false);
        public void SetTwoHandedWeapon() => SetWeapon(false, true);
        public void RemoveWeapon() => SetWeapon(false, false);

        private void SetWeapon(bool hasOneHanded, bool hasTwoHanded)
        {
            _animator.SetBool(s_hasOneHanded, hasOneHanded);
            _animator.SetBool(s_hasTwoHanded, hasTwoHanded);
        }
    }
}