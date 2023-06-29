using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class WeaponChestMarker : MonoBehaviour
    {
        [SerializeField] private Chest _chest;
        [SerializeField] private ParticleSystem _markerEffect;

        private void OnEnable() => 
            _chest.Interacted += OnInteracted;

        private void OnDisable() => 
            _chest.Interacted -= OnInteracted;

        private void OnInteracted()
        {
            _markerEffect.Stop();
            enabled = false;
        }
    }
}