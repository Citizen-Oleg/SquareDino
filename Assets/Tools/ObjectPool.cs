using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    public class ObjectPool<T> where T : Component
    {
        private readonly List<T> _pooledObjects = new List<T>();
    
        private readonly T _poolObject;
        private readonly float _amount;
        private readonly Transform _container;

        public ObjectPool(T poolObject, Transform container, int amount = 5)
        {
            _poolObject = poolObject;
            _amount = amount;
            _container = container;
            
            PoolCreator();
        }
        
        private void PoolCreator()
        {
            for (int i = 0; i < _amount; i++)
            {
                CreateObject(_poolObject);
            }
        }

        public T GetInactiveObject()
        {
            foreach (var inactiveObject in _pooledObjects.Where(poolObject => !poolObject.gameObject.activeInHierarchy))
            {
                return inactiveObject;
            }

            return CreateObject(_poolObject);
        }

        public T Get()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].gameObject.activeInHierarchy)
                {
                    _pooledObjects[i].gameObject.SetActive(true);
                    return _pooledObjects[i];
                }
            }

            var newObject = CreateObject(_pooledObjects.First());
            newObject.gameObject.SetActive(true);

            return newObject;
        }

        private T CreateObject(T gameObject)
        {
            var instantiate = Object.Instantiate(gameObject, _container);

            instantiate.name = gameObject.name;
            instantiate.gameObject.SetActive(false);
            _pooledObjects.Add(instantiate);

            return instantiate;
        }

        public void ClearPool()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                Object.Destroy(pooledObject);
            }
        
            _pooledObjects.Clear();
        }
    }
}