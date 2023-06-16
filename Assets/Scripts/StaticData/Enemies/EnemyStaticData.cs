using System;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Static Data/New enemy")]

    public class EnemyStaticData : ScriptableObject, IStaticData
    {
        [Header ("Stats")]
        public EnemyId Id;
        public GameObject Prefab;
        public float Danger;
        public int Health;
        public int Damage;
        public float Speed;
        public float AttackCooldown;
        public int Coins;

        [Header("Range enemy stats")]
        public int BulletInBurst;

        [Header("Taran enemy stats")]
        public float ChargeSpeedMultiplication;

        [Header("Mine enemy stats")]
        public float LifeTime;

        [Header("Audio")] 
        public AudioClip Sound;

        public Enum Key => Id;
    }
}
