using System;
using Roguelike.Infrastructure.Services;

namespace Roguelike.Ads
{
    public interface IAdsService : IService
    {
        void ShowVideoAd(Action onRewardedCallback = null);
        
        public bool IsVideoOpen { get; }
    }
}