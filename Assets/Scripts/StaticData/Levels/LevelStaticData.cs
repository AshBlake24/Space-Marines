using Roguelike.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New level", menuName = "Static Data/Level/New level")]

    public class LevelStaticData : ScriptableObject
    {
        [Header("Stats")]
        public StageId Id;
        public StageId NextStageId;
        public int ArenasCount;
        public float MinEncounterComplexity;
        public float MaxEncounterComplexity;
        public int TreasureRoomsCount;
        public GameObject LevelGeneratorPrefab;

        public Room StartRoom;
        public List<Room> TransitionRoom;
        public List<Room> Arenas;
        public List<Room> Corridor;
        public List<Room> BonusRoom;
    }
}
