using System;
using Roguelike.Infrastructure.Services;

namespace Roguelike.Ads
{
    public interface IAdsService : IService
    {
        void ShowVideoAd(Action onRewardedCallback = null);
        void ShowInterstitialAd();

        public bool IsVideoOpen { get; }
    }
}