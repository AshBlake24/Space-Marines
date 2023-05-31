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
        [Header("Stats")]
        public EnhancementId Id;
        public TierInfo[] Tiers;

        [Header("Info")] 
        public Sprite Icon;
        public string Name;
        public string Description;

        public Enum Key => Id;
    }
}