using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class WeaponChest : Chest
    {
        public override void Interact(GameObject interactor)
        {
            if (IsActive)
            {
                OnInteracted();
                IsActive = false;
                Outline.enabled = false;
                GameObject weapon = LootFactory.CreateRandomWeapon(transform.position);
                weapon.transform.rotation = transform.rotation;
                
                if (interactor.TryGetComponent(out PlayerStatisticsObserver playerStatistics))
                    playerStatistics.OnChestOpened();
            }
        }
    }
}