namespace Roguelike.Enemies.Transitions
{
    public class AmmoEmpty : Transition
    {
        private Enemy _enemy;

        private void OnEnable()
        {
            if (_enemy == null)
                _enemy = GetComponent<EnemyStateMachine>().Enemy;

            _enemy.NeedReloaded += OnNeedReloaded;
        }

        private void OnDisable()
        {
            _enemy.NeedReloaded -= OnNeedReloaded;
        }

        private void OnNeedReloaded()
        {
            NeedTransit?.Invoke(targetState);
        }
    }
}
