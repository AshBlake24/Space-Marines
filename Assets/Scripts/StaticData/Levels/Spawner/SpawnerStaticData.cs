using System;
using Roguelike.StaticData.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Levels.Spawner
{
    [CreateAssetMenu(fileName = "New spawner", menuName = "Static Data/Level/New spawner")]
    public class SpawnerStaticData : ScriptableObject
    {
        [Header("Stats")]
        public List<EnemyId> Enemies;
        public float Complexity;
        public int MinSpawnPointsInWave;
        public int MaxSpawnPointsInWave;

        private void OnValidate()
        {
            if (MinSpawnPointsInWave <= 0)
                MinSpawnPointsInWave = 1;

            if (MaxSpawnPointsInWave < MinSpawnPointsInWave)
                MaxSpawnPointsInWave = MinSpawnPointsInWave;
        }
    }
}
