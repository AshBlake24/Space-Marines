using Roguelike.Loot.Powerups;
using UnityEngine;

namespace Roguelike.Audio.Sounds.Loot
{
    public class PowerUpAudioPlayer : AudioPlayer
    {
        [Header("Trigger")]
        [SerializeField] private Powerup _powerup;

        private void OnEnable() => 
            _powerup.Collected += OnCollected;

        private void OnDisable() => 
            _powerup.Collected -= OnCollected;
        
        private void OnCollected() => PlayAudio();
    }
}