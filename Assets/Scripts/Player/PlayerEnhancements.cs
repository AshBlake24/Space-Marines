using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player.Enhancements;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerEnhancements : MonoBehaviour, IProgressWriter
    {
        private readonly HashSet<IEnhancement> _enhancements = new();
        private readonly Dictionary<EnhancementId, ParticleSystem> _effects = new();

        private IEnhancementFactory _enhancementFactory;
        private IStaticDataService _staticData;

        public IReadOnlyCollection<IEnhancement> Available => _enhancements;

        public event Action Updated;

        public void Construct(IEnhancementFactory enhancementFactory, IStaticDataService staticData)
        {
            _enhancementFactory = enhancementFactory;
            _staticData = staticData;
        }

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
            TryAddPlayerEffect(enhancementId, tier);
            Updated?.Invoke();
        }

        public void Upgrade(EnhancementId enhancementId, int tier)
        {
            IEnhancement enhancement = _enhancements.Single(enhancement => enhancement.Id == enhancementId);
            enhancement.Upgrade();
            TryAddPlayerEffect(enhancementId, tier);
            Updated?.Invoke();
        }

        public bool EnhancementExist(EnhancementId enhancementId) =>
            _enhancements.Any(item => item.Id == enhancementId);

        private void Cleanup()
        {
            foreach (IEnhancement enhancement in _enhancements)
                enhancement.Cleanup();
        }

        private void TryAddPlayerEffect(EnhancementId enhancementId, int tier)
        {
            EnhancementStaticData data = _staticData.GetDataById<EnhancementId, EnhancementStaticData>(enhancementId);

            if (data.Tiers[tier - 1].PlayerEffect != null)
            {
                if (_effects.TryGetValue(data.Id, out ParticleSystem effect)) 
                    Destroy(effect.gameObject);
                
                _effects[data.Id] = Instantiate(data.Tiers[tier - 1].PlayerEffect, transform);
            }
        }
    }
}