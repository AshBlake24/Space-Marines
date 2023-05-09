using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Powerups.Logic;
using Unity.Mathematics;
using UnityEngine;

namespace Roguelike.Powerups
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField] private PowerupEffect _powerupEffect;
        [SerializeField] private GameObject _model;

        private readonly ParticlesPool _particlesPool;
        private ParticleSystem _vfx;
        private GameObject _target;
        private bool _collected;

        public void Init(ParticleSystem vfx) => 
            _vfx = vfx;

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

        private void HideModel() => 
            _model.SetActive(false);

        private void SpawnVFX() =>
            _vfx = Instantiate(_vfx, _target.transform.position, Quaternion.identity, transform);
    }
}