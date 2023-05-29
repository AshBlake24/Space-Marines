using System;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Enhancements
{
    [CreateAssetMenu(
        fileName = "New Enhancement", 
        menuName = "Static Data/Player/Enhancements/New Enhancement", 
        order = 0)]
    public class EnhancementStaticData : ScriptableObject, IStaticData
    {
        public EnhancementId Id;
        [Range(1, 5)] public int MaxTier;
        public int[] ValuesOnTiers;

        public Enum Key => Id;
    }
}