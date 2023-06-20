using System;
using System.Linq;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New level", menuName = "Static Data/Level/Region")]
    public class RegionStaticData : ScriptableObject, IStaticData
    {
        public LevelId Id;
        public LocalizedString Name;
        public Sprite Icon;
        public Floor[] Floors;
        
        [Range(1, 5)] 
        public int Difficulty;
        
        public Enum Key => Id;
        public int StagesCount => Floors.SelectMany(floor => floor.Stages).Count();
        
        [Serializable]
        public struct Floor
        {
            public StageStaticData[] Stages;
        }
    }
}