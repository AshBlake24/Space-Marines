using UnityEngine;

namespace Roguelike.Enemies
{
    public class BossStateMachine : EnemyStateMachine
    {
        [SerializeField] private BossRoot _bossRoot;

        public BossRoot BossRoot => _bossRoot;
    }
}