using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private IParticlesPoolService _particlesPool;
        private PowerupEffect _powerupEffect;
        private ParticleSystem _vfx;
        private GameObject _target;
        private bool _collected;
        private string _vfxKey;

        public void Construct(IParticlesPoolService particlesPool, PowerupEffect powerupDataEffect, ParticleSystem vfx)
        {
            _powerupEffect = powerupDataEffect;
            _vfxKey = vfx.gameObject.name;
            CreateVFXPool(particlesPool, vfx);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collected)
                return;
            
            TryApplyPowerup(other.gameObject);
        }

        private void LateUpdate()
        {
            if (_collected)
                _vfx.transform.position = _target.transform.position;
        }

        private void TryApplyPowerup(GameObject target)
        {
            if (_powerupEffect.TryApply(target, () => Destroy(gameObject)))
            {
                _collected = true;
                _target = target;
                HideModel();
                SpawnVFX();
            }
            else
            {
                _collected = false;
            }
        }

        private void SpawnVFX()
        {
            _vfx = _particlesPool.GetInstance(_vfxKey);
            _vfx.transform.position = _target.transform.position;

            if (_powerupEffect is ILastingEffect lastingEffect)
                _vfx.GetComponent<ReturnToPool>().StartLastingEffect(lastingEffect.Duration);
            
            _vfx.Play();
        }

        private void CreateVFXPool(IParticlesPoolService particlesPool, ParticleSystem vfx)
        {
            _particlesPool = particlesPool;
            _particlesPool.CreateNewPool(_vfxKey, vfx, 3, 10);
        }

        private void HideModel() => 
            _view.SetActive(false);
    }
}