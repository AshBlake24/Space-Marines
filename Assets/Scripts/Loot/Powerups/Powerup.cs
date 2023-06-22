using System;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    public class Powerup : MonoBehaviour
    {
        private PowerupId _id;
        private IParticlesPoolService _particlesPool;
        private PowerupEffect _powerupEffect;
        private Statistics _statistics;
        private ParticleSystem _vfx;
        private bool _collected;
        private string _vfxKey;

        public event Action Collected;

        private void OnDestroy() => 
            Destroy(gameObject);

        public void Construct(IParticlesPoolService particlesPool, PowerupId id, PowerupEffect powerupDataEffect, 
            Statistics statistics, ParticleSystem vfx)
        {
            _id = id;
            _statistics = statistics;
            _vfxKey = vfx.gameObject.name;
            _powerupEffect = powerupDataEffect;
            CreateVFXPool(particlesPool, vfx);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collected)
                return;
            
            TryApplyPowerup(other.gameObject);
        }

        private void TryApplyPowerup(GameObject target)
        {
            if (_powerupEffect.TryApply(target))
            {
                _statistics.CollectablesData.CollectPowerup(_id);
                _collected = true;
                Collected?.Invoke();
                SpawnVFX(target.transform);
                Destroy(gameObject);
            }
            else
            {
                _collected = false;
            }
        }

        private void SpawnVFX(Transform target)
        {
            _vfx = _particlesPool.GetInstance(_vfxKey);
            _vfx.transform.SetParent(target);
            _vfx.transform.localPosition = Vector3.zero;

            if (_powerupEffect is ILastingEffect lastingEffect)
                _vfx.GetComponent<ReturnToPool>().StartLastingEffect(lastingEffect.Duration);
            
            _vfx.Play();
        }

        private void CreateVFXPool(IParticlesPoolService particlesPool, ParticleSystem vfx)
        {
            _particlesPool = particlesPool;
            _particlesPool.CreateNewPool(_vfxKey, vfx, defaultSize: 3, maxSize: 10);
        }
    }
}