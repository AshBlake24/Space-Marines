using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Logic;
using Roguelike.Player;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Powerups.Logic
{
    [CreateAssetMenu(fileName = "Speed Booster", menuName = "Static Data/Powerups/Speed Booster", order = 2)]
    public class SpeedBooster : PowerupEffect, ILastingEffect
    {
        [SerializeField, Range(1f,2f)] private float _speedMultiplier;
        [SerializeField, Range(1f,60f)] private float _duration;
        
        private ICoroutineRunner _coroutineRunner;

        public void Construct(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public override bool TryApply(GameObject target, Action onComplete)
        {
            if (target.TryGetComponent(out PlayerMovement playerMovement))
            {
                if (playerMovement.Boosted == false)
                {
                    _coroutineRunner.StartCoroutine(EffectDuration(playerMovement, onComplete));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator EffectDuration(PlayerMovement playerMovement, Action onComplete)
        {
            playerMovement.BoostSpeed(_speedMultiplier);

            yield return Helpers.GetTime(_duration);
            
            playerMovement.ResetSpeed();
            onComplete?.Invoke();
        }
    }
}