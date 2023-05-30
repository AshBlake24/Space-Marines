using UnityEngine;

namespace Roguelike.Roguelike.Enemies.Animators
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int s_speed = Animator.StringToHash("Speed");
        private static readonly int s_isStopped = Animator.StringToHash("IsStopped");
        private static readonly int s_attack = Animator.StringToHash("Attack");
        private static readonly int s_died = Animator.StringToHash("Died");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(float speed, bool isStopped)
        {
            _animator.SetBool(s_isStopped, isStopped);
            _animator.SetFloat(s_speed, speed);
        }

        public void PlayAttack() => _animator.SetTrigger(s_attack);

        public void PlayDie() => _animator.SetTrigger(s_died);

    }
}
