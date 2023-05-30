using Roguelike.Infrastructure.Services;
using Roguelike.Player.Enhancements;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IEnhancementFactory : IService
    {
        IEnhancement CreateEnhancement(EnhancementId id, GameObject player);
    }
}