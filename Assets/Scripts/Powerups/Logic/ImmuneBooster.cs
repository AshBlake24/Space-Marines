using System;
using Roguelike.Infrastructure;
using Roguelike.Logic;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Powerups.Logic
{
    [CreateAssetMenu(fileName = "Immune Booster", menuName = "Static Data/Powerups/Immune Booster", order = 2)]
    public class ImmuneBooster : PowerupEffect, ILastingEffect
    {
        [SerializeField, Range(1f, 60f)] private float _duration;
        
        private ICoroutineRunner _coroutineRunner;

        public void Construct(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public override bool TryApply(GameObject target, Action onComplete)
        {
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                if (playerHealth.IsImmune == false)
                {
                    _coroutineRunner.StartCoroutine(playerHealth.ImmuneTimer(_duration));
                    return true;
                }
            }

            return false;
        }
    }
}