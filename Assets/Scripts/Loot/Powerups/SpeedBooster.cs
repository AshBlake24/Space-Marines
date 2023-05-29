using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Player;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Movement Speed Buff", 
        menuName = "Static Data/Powerups/Effects/Speed Booster", 
        order = 1)]
    public class SpeedBooster : PowerupEffect, ILastingEffect
    {
        [SerializeField, Range(1f,2f)] private float _speedMultiplier;
        [SerializeField, Range(1f,60f)] private float _duration;
        
        private ICoroutineRunner _coroutineRunner;

        public float Duration => _duration;

        public void Construct(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public override bool TryApply(GameObject target)
        {
            if (target.TryGetComponent(out PlayerMovement playerMovement))
            {
                if (playerMovement.Boosted == false)
                {
                    _coroutineRunner.StartCoroutine(EffectDuration(playerMovement));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator EffectDuration(PlayerMovement playerMovement)
        {
            playerMovement.BoostSpeed(_speedMultiplier);

            yield return Helpers.GetTime(_duration);
            
            playerMovement.ResetSpeed();
        }
    }
}