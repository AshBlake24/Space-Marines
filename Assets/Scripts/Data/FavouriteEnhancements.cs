using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Data
{
    [Serializable]
    public class FavouriteEnhancements
    {
        public EnhancementId Enhancement;
        
        public void SetFavouriteEnhancement(EnhancementId enhancementId)
        {
            Enhancement = enhancementId;
        }
    }
}