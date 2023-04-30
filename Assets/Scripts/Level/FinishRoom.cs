using System;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Level
{
    public class FinishRoom : Room
    {
        [SerializeField] private EnterTriger _enterTriger;

        public event Action PlayerFinishedLevel;

        private void OnEnable()
        {
            _enterTriger.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnDisable()
        {
            _enterTriger.PlayerHasEntered -= OnPlayerHasEntered;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            PlayerFinishedLevel?.Invoke();
        }
    }
}
