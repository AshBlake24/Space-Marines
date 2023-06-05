using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player.Enhancements;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerEnhancements : MonoBehaviour, IProgressWriter
    {
        private readonly HashSet<IEnhancement> _enhancements = new();

        private IEnhancementFactory _enhancementFactory;

        public IReadOnlyCollection<IEnhancement> Available => _enhancements;

        public event Action Updated;

        public void Construct(IEnhancementFactory enhancementFactory) =>
            _enhancementFactory = enhancementFactory;

        private void OnDestroy() => Cleanup();

        public void ReadProgress(PlayerProgress progress)
        {
            if (_enhancements.Count > 0)
                throw new ArgumentOutOfRangeException(nameof(_enhancements),
                    "Collection must be clear before initialization");

            foreach (EnhancementData enhancementData in progress.State.Enhancements)
                AddEnhancement(enhancementData.Id, enhancementData.Tier);
        }

        public void WriteProgress(PlayerProgress progress)
        {
            foreach (IEnhancement enhancement in _enhancements)
            {
                EnhancementData enhancementData = progress.State.Enhancements
                    .SingleOrDefault(item => item.Id == enhancement.Id);

                if (enhancementData == null)
                    progress.State.Enhancements.Add(new EnhancementData(enhancement.Id, enhancement.CurrentTier));
                else
                    enhancementData.Tier = enhancement.CurrentTier;
            }
        }

        public void AddEnhancement(EnhancementId enhancementId, int tier)
        {
            if (_enhancements.Any(item => item.Id == enhancementId))
                throw new ArgumentOutOfRangeException(nameof(enhancementId), "This enhancement already exists");

            IEnhancement enhancement = _enhancementFactory.CreateEnhancement(enhancementId, tier, gameObject);
            enhancement.Apply();
            _enhancements.Add(enhancement);
            Updated?.Invoke();
        }

        public bool EnhancementExist(EnhancementId enhancementId) =>
            _enhancements.Any(item => item.Id == enhancementId);

        private void Cleanup()
        {
            foreach (IEnhancement enhancement in _enhancements)
                enhancement.Cleanup();
        }

        public void Upgrade(EnhancementId enhancementId, int i)
        {
            IEnhancement enhancement = _enhancements.Single(enhancement => enhancement.Id == enhancementId);
            enhancement.Upgrade();
            Updated?.Invoke();
        }
    }
}