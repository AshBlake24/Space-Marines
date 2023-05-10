using System;
using System.Collections;
using Roguelike.Infrastructure;
using Roguelike.Logic;
using Roguelike.Player;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Immune", 
        menuName = "Static Data/Powerups/Effects/Immune Booster", 
        order = 1)]
    public class ImmuneBooster : PowerupEffect, ILastingEffect
    {
        [SerializeField, Range(1f, 60f)] private float _duration;
        
        private ICoroutineRunner _coroutineRunner;
        
        public float Duration => _duration;

        public void Construct(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public override bool TryApply(GameObject target, Action onComplete)
        {
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                if (playerHealth.IsImmune == false)
                {
                    _coroutineRunner.StartCoroutine(EffectDuration(playerHealth, onComplete));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator EffectDuration(PlayerHealth playerHealth, Action onComplete)
        {
            playerHealth.SetImmune(true);

            yield return Helpers.GetTime(_duration);
            
            playerHealth.SetImmune(false);
            onComplete?.Invoke();
        }
    }
}