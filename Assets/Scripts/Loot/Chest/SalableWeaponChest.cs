using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class SalableWeaponChest : Chest
    {
        private IWindowService _windowService;

        public override void Interact(GameObject interactor)
        {
            if (IsActive)
                _windowService.OpenWeaponChestWindow(this);
        }

        public void Open()
        {
            if (IsActive)
            {
                OnInteracted();
                IsActive = false;
                Outline.enabled = false;
                GameObject weapon = LootFactory.CreateRandomWeapon(transform.position);
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