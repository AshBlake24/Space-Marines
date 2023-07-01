using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Roguelike.Enemies.Animators
{
    public class EnemyAnimator : MonoBehaviour
    {
        private const string ClipName = "Death";

        private static readonly int s_enemySpeed = Animator.StringToHash("EnemySpeed");
        private static readonly int s_isStopped = Animator.StringToHash("IsStopped");
        private static readonly int s_isWait = Animator.StringToHash("IsWait");
        private static readonly int s_attackOptional = Animator.StringToHash("OptionalAttack");
        private static readonly int s_attack = Animator.StringToHash("Attack");
        private static readonly int s_die = Animator.StringToHash("Died");
        private static readonly int s_hit = Animator.StringToHash("Hit");

        private Animator _animator;
        private bool _isDied;
<<<<<<< HEAD
=======
        private bool _isDeathPlayed;
>>>>>>> 47a1392b186820f0d9c1b465a2b09d9fecdcbbab
        private AnimatorClipInfo[] _currentClips;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_isDied == false)
                return;

<<<<<<< HEAD
=======
            if (_isDeathPlayed)
                return;

>>>>>>> 47a1392b186820f0d9c1b465a2b09d9fecdcbbab
            _currentClips = _animator.GetCurrentAnimatorClipInfo(0);

            for (int i = 0; i < _currentClips.Length; i++)
            {
                if (_currentClips[i].clip.name == ClipName)
<<<<<<< HEAD
                    enabled = false;
=======
                    _isDeathPlayed = true;
>>>>>>> 47a1392b186820f0d9c1b465a2b09d9fecdcbbab
                else
                    PlayDie();
            }
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
            _animator.SetTrigger(s_die);
            _isDied = true;

            enabled = true;
        }
        
        public void PlayIdle(bool isWait) => _animator.SetBool(s_isWait, isWait);
        public void PlayOptionalAttack() => _animator.SetTrigger(s_attackOptional);
    }
}
