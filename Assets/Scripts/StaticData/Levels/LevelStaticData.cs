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
        public StageId NextLevelId;
        public int AreaRoomCount;
        public float MinEncounterComplexity;
        public float MaxEncounterComplexity;
        public int BonusRoomCount;
        public GameObject LevelGeneratorPrefab;

        public Room StartRoom;
        public List<Room> FinishRoom;
        public List<Room> AreaRooms;
        public List<Room> Corridor;
        public List<Room> BonusRoom;
    }
}
