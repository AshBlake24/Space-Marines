using Roguelike.StaticData.Enemies;
using System.Collections;
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
    }
}
