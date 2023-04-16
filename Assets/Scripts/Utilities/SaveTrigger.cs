using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider Collider;
        [SerializeField] private  Color Color;
        
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved!");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (Collider == false)
                return;
            
            Gizmos.color = Color;
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}
