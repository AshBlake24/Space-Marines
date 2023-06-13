using Roguelike.Enemies.EnemyStates;
using UnityEngine;

namespace Roguelike.Audio.Sounds.Enemies
{
    public class EnemyExplosionAudioPlayer : AudioPlayer
    {
        [Header("Trigger")]
        [SerializeField] private ExplosionState _explosionState;

        private void OnEnable() => 
            _explosionState.Exploded += OnExploded;

        private void OnDisable() => 
            _explosionState.Exploded -= OnExploded;
        
        private void OnExploded() => PlayAudio();
    }
}