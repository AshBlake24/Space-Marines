using Roguelike.Player;

namespace Roguelike.Enemies.Transitions
{
    public class PlayerResurected : Transition
    {
        private PlayerDeath _player;

        private void OnEnable()
        {
            if (_player == null)
            {
                EnemyStateMachine stateMachine = GetComponent<EnemyStateMachine>();
                _player = stateMachine.Enemy.Target.GetComponent<PlayerDeath>();
            }

            _player.Resurrected += OnPlayerResurrected;
        }

        private void OnDisable()
        {
            _player.Resurrected -= OnPlayerResurrected;
        }

        private void OnPlayerResurrected()
        {
            NeedTransit?.Invoke(targetState);
        }
    }
}