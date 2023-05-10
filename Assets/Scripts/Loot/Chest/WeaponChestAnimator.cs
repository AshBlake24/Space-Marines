using UnityEngine;

namespace Roguelike.Loot.Chest
{
    [RequireComponent(typeof(WeaponChest))]
    public class WeaponChestAnimator : MonoBehaviour
    {
        private static readonly int s_open = Animator.StringToHash("Open");

        [SerializeField] private WeaponChest _weaponChest;
        [SerializeField] private Animator _animator;

        private void OnEnable() => 
            _weaponChest.Interacted += OnInteracted;

        private void OnDisable() => 
            _weaponChest.Interacted -= OnInteracted;

        private void OnInteracted() => 
            _animator.SetTrigger(s_open);
    }
}