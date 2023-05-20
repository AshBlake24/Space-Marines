using DG.Tweening;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class WeaponChestAnimator : MonoBehaviour
    {
        [SerializeField] private float _openingDuration;
        [SerializeField] private Vector3 _openAngle;
        [SerializeField] private Transform _hinge;
        [SerializeField] private WeaponChest _weaponChest;

        private void OnEnable() => 
            _weaponChest.Interacted += OnInteracted;

        private void OnDisable() => 
            _weaponChest.Interacted -= OnInteracted;

        private void OnInteracted() => 
            _hinge.DOLocalRotate(_openAngle, _openingDuration).SetEase(Ease.InOutQuad);
    }
}