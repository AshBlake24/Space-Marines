using Roguelike.Weapons.Projectiles;
using UnityEngine;

namespace Roguelike.Audio.Sounds.Weapons
{
    public class ProjectileAudioPlayer : AudioPlayer
    {
        [Header("Trigger")]
        [SerializeField] private Projectile _projectile;

        private void OnEnable() => 
            _projectile.Impacted += OnImpacted;

        private void OnDisable() => 
            _projectile.Impacted -= OnImpacted;
        
        private void OnImpacted() => PlayAudio();
    }
}