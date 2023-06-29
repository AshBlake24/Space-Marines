using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Logic.Interactables;
using Roguelike.StaticData.Loot.Rarity;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public abstract class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] protected RarityId MinimalRarity;
        [SerializeField] private Outline _outline;

        protected ILootFactory LootFactory;

        public event Action Interacted;

        public Outline Outline => _outline;
        public bool IsActive { get; protected set; }

        private void Awake() => Initialize();

        public abstract void Interact(GameObject interactor);

        protected virtual void Initialize()
        {
            LootFactory = AllServices.Container.Single<ILootFactory>();
            Outline.enabled = false;
            IsActive = true;
        }

        protected void OnInteracted() => Interacted?.Invoke();
    }
}