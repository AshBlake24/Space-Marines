using Roguelike.Enemies.EnemyStates;
using UnityEngine;

namespace Roguelike.Audio.Sounds.Enemies
{
    public class RangeAttackAudioPlayer : AudioPlayer
    {
        [Header("Trigger")]
        [SerializeField] private RangeAttackState _rangeAttackState;

        private void OnEnable() => 
            _rangeAttackState.Fired += OnFired;

        private void OnDisable() => 
            _rangeAttackState.Fired -= OnFired;
        
        private void OnFired() => PlayAudio();
    }
}