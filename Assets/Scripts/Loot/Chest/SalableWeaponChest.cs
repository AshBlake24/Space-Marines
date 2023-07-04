using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class SalableWeaponChest : Chest
    {
        private IWindowService _windowService;

        public event Action Opened;

        public override void Interact(GameObject interactor)
        {
            if (IsActive)
            {
                _windowService.OpenWeaponChestWindow(this);
                OnInteracted();
            }
        }

        public void Open()
        {
            if (IsActive)
            {
                Opened?.Invoke();
                IsActive = false;
                Outline.enabled = false;
                GameObject weapon = LootFactory.CreateRandomWeapon(transform.position, MinimalRarity);
                weapon.transform.rotation = transform.rotation;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _windowService = AllServices.Container.Single<IWindowService>();
        }
    }
}