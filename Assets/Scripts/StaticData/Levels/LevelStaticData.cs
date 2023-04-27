using Roguelike.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New level", menuName = "Static Data/Level/New level")]

    public class LevelStaticData : ScriptableObject
    {
        [Header("Stats")]
        public LevelId Id;
        public int AreaRoomCount;
        public int BonusRoomCount;
        public GameObject LevelGeneratorPrefab;

        public Room StartRoom;
        public List<Room> AreaRooms;
        public List<Room> Corridor;
        public List<Room> BonusRoom;
    }
}
