using Roguelike.Level;
using Roguelike.StaticData.Levels.Spawner;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Static Data/Level/Stage")]

    public class StageStaticData : ScriptableObject
    {
        [Header("Stats")]
        public StageId Id;
        public StageId NextStageId;
        public GameObject LevelGeneratorPrefab;
        public int ArenasCount;
        public int TreasureRoomsCount;
        public float MinComplexityMultiplication;
        public float MaxComplexityMultiplication;
        public SpawnerStaticData Spawner;

        [Header("Rooms")]
        public Room StartRoom;
        public List<Room> TransitionRoom;
        public List<Room> Arenas;
        public List<Room> Corridor;
        public List<Room> BonusRoom;
    }
}
