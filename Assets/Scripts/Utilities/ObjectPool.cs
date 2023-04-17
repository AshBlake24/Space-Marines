using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ObjectPool<TKey, TSource> where TSource : Component
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Queue<TSource> _pool = new();

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab), "Prefab can't be null");

            if (prefab.GetComponent<TSource>() == null)
                throw new ArgumentNullException(nameof(prefab), $"Prefab does not have the {typeof(TSource)} component");

            _prefab = prefab;
            _container = new GameObject($"Pool - {typeof(TKey)}").transform;
            _container.SetParent(Helpers.GetGeneralPoolsContainer());
        }

        public void AddInstance(TSource instance)
        {
            _pool.Enqueue(instance);
            instance.gameObject.SetActive(false);
        }

        public TSource GetInstance()
        {
            return _pool.Count <= 0 ? CreateInstance() : _pool.Dequeue();
        }

        private TSource CreateInstance()
        {
            GameObject instance = UnityEngine.Object.Instantiate(_prefab, _container);

            return instance.GetComponent<TSource>();
        }
    }
}