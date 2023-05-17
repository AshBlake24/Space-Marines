using Roguelike.Enemies.EnemyStates;
using Roguelike.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies.Transitions
{
    public class AmmoEmpty : Transition
    {
        private Enemy _enemy;

        private void OnEnable()
        {
            if (_enemy == null)
                _enemy = GetComponent<EnemyStateMachine>().Enemy;

            _enemy.NeedReloaded += NeedReloaded;
        }

        private void OnDisable()
        {
            _enemy.NeedReloaded -= NeedReloaded;
        }

        private void NeedReloaded()
        {
            NeedTransit?.Invoke(targetState);
        }
    }
}
