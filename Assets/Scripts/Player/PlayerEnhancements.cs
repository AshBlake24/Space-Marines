using System;
using System.Collections.Generic;
using Roguelike.Infrastructure.Factory;
using Roguelike.Player.Enhancements;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerEnhancements : MonoBehaviour
    {
        private readonly List<IEnhancement> _enhancements = new();
        
        private IEnhancementFactory _enhancementFactory;

        public void Construct(IEnhancementFactory enhancementFactory) => 
            _enhancementFactory = enhancementFactory;

        public void AddEnhancement(EnhancementId enhancementId)
        {
            if (_enhancements.Exists(item => item.Id == enhancementId))
                throw new ArgumentOutOfRangeException(nameof(enhancementId), "This enhancement already exists");

            IEnhancement enhancement = _enhancementFactory.CreateEnhancement(enhancementId, gameObject);
            _enhancements.Add(enhancement);
        }
        
        private void Cleanup()
        {
            foreach (IEnhancement enhancement in _enhancements)
                enhancement.Cleanup();
        }
    }
}