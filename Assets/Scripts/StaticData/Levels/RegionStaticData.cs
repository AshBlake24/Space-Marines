using System;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New level", menuName = "Static Data/Level/Region")]
    public class RegionStaticData : ScriptableObject, IStaticData
    {
        public LevelId Id;
        public string Name;
        public StageStaticData[] Stages;
        
        [Range(1, 5)] 
        public int Difficulty;
        
        public Enum Key => Id;
    }
}