using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemies.Traps
{
    [RequireComponent(typeof(SphereCollider))]
    public class Mine : MonoBehaviour
    {
        private const string ExplosionEffectKey = nameof(ExplosionEffectKey);

        [SerializeField] private float _lifetime;
        [SerializeField, Range(1, 3)] private int _damage;
        [SerializeField] private ParticleSystem _explosionEffect;

        private IParticlesPoolService _particlesPoolService;
        private float _elapsedTime;

        private void Awake() => InitEffectPool();

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime > _lifetime)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider interactor)
        {
            if (interactor.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                SpawnVFX();
                playerHealth.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        private void SpawnVFX()
        {
            ParticleSystem particles = _particlesPoolService.GetInstance(ExplosionEffectKey);
            particles.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            particles.Play();
        }

        private void InitEffectPool()
        {
            _particlesPoolService = AllServices.Container.Single<IParticlesPoolService>();
            _particlesPoolService.CreateNewPool(ExplosionEffectKey, _explosionEffect);
        }
    }
}