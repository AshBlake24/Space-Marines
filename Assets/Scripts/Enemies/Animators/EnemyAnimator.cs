using UnityEngine;

namespace Roguelike.Roguelike.Enemies.Animators
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int s_enemySpeed = Animator.StringToHash("EnemySpeed");
        private static readonly int s_clipSpeed = Animator.StringToHash("ClipSpeed");
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
            _animator.SetFloat(s_enemySpeed, speed);
        }

        public void PlayAttack() => _animator.SetTrigger(s_attack);

        public void PlayDie() => _animator.SetTrigger(s_died);

        public void SetClipSpeed(float speed) => _animator.SetFloat(s_clipSpeed, speed);
    }
}
