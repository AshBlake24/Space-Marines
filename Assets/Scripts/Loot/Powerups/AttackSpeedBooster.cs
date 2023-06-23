using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Player;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Attack Speed Buff", 
        menuName = "Static Data/Powerups/Effects/Attack Speed Boost", 
        order = 1)]
    public class AttackSpeedBooster : PowerupEffect, ILastingEffect
    {
        [SerializeField, Range(0f, 1f)] private float _speedMultiplier;
        [SerializeField, Range(1f, 60f)] private float _duration;
        
        private ICoroutineRunner _coroutineRunner;
        
        public float Duration => _duration;

        public void Construct(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;
        
        public override bool TryApply(GameObject target)
        {
            if (target.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.Boosted == false)
                {
                    _coroutineRunner.StartCoroutine(EffectDuration(playerShooter));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator EffectDuration(PlayerShooter playerShooter)
        {
            playerShooter.SetAttackSpeedMultiplier(_speedMultiplier);

            yield return Helpers.GetTime(_duration);
            
            playerShooter.ResetAttackSpeedMultiplier();
        }
    }
}