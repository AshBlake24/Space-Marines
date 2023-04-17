using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ObjectPool<TSource> where TSource : Component
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Queue<TSource> _pool;

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab), "Prefab can't be null");

            if (prefab.GetComponent<TSource>() == null)
                throw new ArgumentNullException(nameof(prefab), $"Prefab does not have the {typeof(TSource)} component");

            _pool = new Queue<TSource>();
            _prefab = prefab;

            _container = new GameObject($"Pool - {prefab.name}").transform;
            _container.SetParent(Helpers.GetGeneralPoolsContainer());
        }

        public bool HasObjects => _pool.Count > 0;

        public TSource GetInstance() => _pool.Dequeue();

        public void AddInstance(TSource instance)
        {
            _pool.Enqueue(instance);
            instance.transform.SetParent(_container);
            instance.gameObject.SetActive(false);
        }
    }
}