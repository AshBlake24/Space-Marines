using Roguelike.Enemies;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Roguelike.Logic.Popups
{
    public class DamagePopupViewer : MonoBehaviour
    {
        [SerializeField] private Vector3 _spawnPosition;
        
        private IAssetProvider _assetProvider;
        private IObjectPool<DamagePopup> _popupsPool;

        public void OnEnable()
        {
            _assetProvider = AllServices.Container.Single<IAssetProvider>();
            _popupsPool = new ObjectPool<DamagePopup>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false);
        }

        public void SubscribeToEnemy(EnemyHealth enemyHealth)
        {
            enemyHealth.DamageTook += OnDamageTook;
            enemyHealth.Died += OnEnemyDied;
        }

        private void UnsubscribeFromEnemy(EnemyHealth enemyHealth)
        {
            enemyHealth.DamageTook -= OnDamageTook;
            enemyHealth.Died -= OnEnemyDied;
        }

        private void OnDamageTook(int damage, Transform enemyTransform)
        {
            DamagePopup popup = _popupsPool.Get();
            SetPopupTransform(enemyTransform, popup.transform);
            popup.SetValue(damage);
        }

        private void SetPopupTransform(Transform enemyTransform, Transform popup)
        {
            popup.position = enemyTransform.position + _spawnPosition;
            Vector3 direction = popup.position - GameFactory.PlayerCamera.transform.position;
            popup.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        private DamagePopup CreatePoolItem()
        {
            DamagePopup damagePopup = _assetProvider.Instantiate(AssetPath.DamagePopupPath)
                .GetComponent<DamagePopup>();
            
            damagePopup.Construct(_popupsPool);
            damagePopup.transform.SetParent(Helpers.GetPoolsContainer(nameof(DamagePopup)));
            
            return damagePopup;
        }

        private void OnTakeFromPool(DamagePopup popup)
        {
            popup.gameObject.SetActive(true);
            popup.transform.localScale = Vector3.one;;
        }

        private void OnReleaseToPool(DamagePopup popup) => 
            popup.gameObject.SetActive(false);

        private void OnDestroyItem(DamagePopup popup) => 
            Object.Destroy(popup);

        private void OnEnemyDied(EnemyHealth enemyHealth) => 
            UnsubscribeFromEnemy(enemyHealth);
    }
}