using Roguelike.Level;
using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private EnterTriger _enterTriger;
        [SerializeField] private GameObject _activeView;
        [SerializeField] private int _damage;

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
            player.TakeDamage(_damage);
            _activeView.SetActive(true);
        }
    }
}
