using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Audio.Logic
{
    public class RangedWeaponAudioPlayer : AudioPlayer
    {
        [Header("Trigger")]
        [SerializeField] private RangedWeapon _rangedWeapon;

        private void OnEnable() => 
            _rangedWeapon.Fired += OnFired;

        private void OnDisable() => 
            _rangedWeapon.Fired -= OnFired;
        
        private void OnFired() => PlayAudio();
    }
}