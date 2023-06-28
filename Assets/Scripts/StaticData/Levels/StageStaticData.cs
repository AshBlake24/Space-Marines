using System;
using Roguelike.Level;
using Roguelike.StaticData.Levels.Spawner;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Static Data/Level/Stage")]

    public class StageStaticData : ScriptableObject, IStaticData
    {
        [Header("Stats")]
        public StageId Id;
        public StageId NextStageId;
        public int Score;
        public int ArenasCount;
        public int BonusRoomsMaxCount;
        public float MinComplexityMultiplication;
        public float MaxComplexityMultiplication;
        public SpawnerStaticData Spawner;

        [Header("Rooms")]
        public Room StartRoom;
        public List<Room> TransitionRoom;
        public List<Room> Arenas;
        public List<Room> Corridor;
        public List<Room> BonusRoom;

        public Enum Key => Id;
    }
}
