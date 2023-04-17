using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ObjectPool<TSource> where TSource : Component
    {
        private readonly GameObject _prefab;
        private readonly Queue<TSource> _pool;

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab), "Prefab can't be null");

            if (prefab.GetComponent<TSource>() == null)
                throw new ArgumentNullException(nameof(prefab), $"Prefab does not have the {typeof(TSource)} component");

            _pool = new Queue<TSource>();
            _prefab = prefab;

            Transform container = new GameObject($"Pool - {prefab.name}").transform;
            container.SetParent(Helpers.GetGeneralPoolsContainer());
        }

        public bool HasObjects => _pool.Count > 0;

        public TSource GetInstance() => _pool.Dequeue();

        public void AddInstance(TSource instance)
        {
            _pool.Enqueue(instance);
            instance.gameObject.SetActive(false);
        }
    }
}