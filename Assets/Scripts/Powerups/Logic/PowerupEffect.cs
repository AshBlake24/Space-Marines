using System;
using UnityEngine;

namespace Roguelike.Powerups.Logic
{
    public abstract class PowerupEffect : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ParticleSystem _vfx;
        [SerializeField, Range(0f, 100f)] private float _dropChance;

        public GameObject Prefab => _prefab;
        public ParticleSystem VFX => _vfx;
        public float DropChance => _dropChance;

        public abstract bool TryApply(GameObject target, Action onComplete);
    }
}