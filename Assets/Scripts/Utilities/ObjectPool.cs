using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ObjectPool<T> where T : Component
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _pool = new();

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab), "Prefab can't be null");

            if (prefab.GetComponent<T>() == null)
                throw new ArgumentNullException(nameof(prefab), $"Prefab does not have the {typeof(T)} component");

            _prefab = prefab;
            _container = new GameObject($"Pool - {_prefab.name}").transform;
            _container.SetParent(Helpers.GetGeneralPoolsContainer());
        }

        public void AddInstance(T instance)
        {
            _pool.Enqueue(instance);
            instance.gameObject.SetActive(false);
        }

        public T GetInstance()
        {
            return _pool.Count <= 0 ? CreateInstance() : _pool.Dequeue();
        }

        private T CreateInstance()
        {
            GameObject instance = UnityEngine.Object.Instantiate(_prefab, _container);

            return instance.GetComponent<T>();
        }
    }
}