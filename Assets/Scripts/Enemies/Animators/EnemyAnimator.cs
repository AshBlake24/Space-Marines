using UnityEngine;

namespace Roguelike.Roguelike.Enemies.Animators
{
    [RequireComponent(typeof(Animator))]

    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int s_enemySpeed = Animator.StringToHash("EnemySpeed");
        private static readonly int s_isStopped = Animator.StringToHash("IsStopped");
        private static readonly int s_isWait = Animator.StringToHash("IsWait");
        private static readonly int s_attackOptional = Animator.StringToHash("OptionalAttack");
        private static readonly int s_attack = Animator.StringToHash("Attack");
        private static readonly int s_hit = Animator.StringToHash("Hit");
        private static readonly int s_isDead = Animator.StringToHash("IsDead");

        private Animator _animator;
        private bool _isDied;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(float speed, bool isStopped)
        {
            _animator.SetBool(s_isStopped, isStopped);
            _animator.SetFloat(s_enemySpeed, speed);
        }

        public void PlayAttack() 
        {
            if (_isDied == true)
                return;

            _animator.SetTrigger(s_attack);
        }

        public void PlayHit() => _animator.SetTrigger(s_hit);

        public void PlayDie() 
        {
            _animator.SetBool(s_isDead, true);

            _isDied = true;
        }
        
        public void PlayIdle(bool isWait) => _animator.SetBool(s_isWait, isWait);
        public void PlayOptionalAttack() => _animator.SetTrigger(s_attackOptional);
        public void SetIsDeadToFalse() => _animator.SetBool(s_isDead, false);
    }
}
