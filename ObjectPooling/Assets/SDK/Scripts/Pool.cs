using System.Collections.Generic;
using UnityEngine;

namespace Liminal.ObjectPooling
{
    /// <summary>
    /// A generic Pool for T for Objects - MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> where T : Object, IPoolable<T>
    {
        private T _prefab;
        private int _poolSize = 10;
        private int _growAmount = 0;
        private Transform _container;

        public Pool(T prefab, int size = 5, int growAmount = 5, Transform container = null)
        {
            _prefab = prefab;
            _poolSize = size;
            _growAmount = growAmount;
            _container = container;

            Populate(_poolSize);
        }

        public int Count { get { return _poolList.Count; } }

        protected virtual void Populate(int amount)
        {
            var prefabActiveState = _prefab.gameObject.activeInHierarchy;
            _prefab.gameObject.SetActive(false);

            for (int i = 0; i < amount; i++)
            {
                var instance = Object.Instantiate(_prefab, _container);
                _poolList.Enqueue(instance);

                instance.OnRelease += Release;
            }

            _prefab.gameObject.SetActive(prefabActiveState);
        }

        public void Release(T instance)
        {
            if (instance == null)
                return;

            _poolList.Enqueue(instance);

            instance.gameObject.SetActive(false);
            instance.gameObject.transform.SetParent(_container);
        }

        public T Get(Vector3? position = null, Quaternion? rotation = null, Transform parent = null, bool active = true)
        {
            var instance = Get();

            instance.gameObject.transform.position = position != null ? position.Value : Vector3.zero;
            instance.gameObject.transform.rotation = rotation != null ? rotation.Value : Quaternion.identity;
            instance.gameObject.transform.SetParent(parent);

            instance.gameObject.SetActive(active);

            return instance;
        }

        public T Get()
        {
            if (_poolList.Count <= 0)
            {
                if (_growAmount == 0)
                {
                    Debug.LogFormat("Pool {0} is exahausted", this.GetType());
                    return null;
                }
                else
                    Populate(_growAmount);
            }

            return _poolList.Dequeue();
        }

        private Queue<T> _poolList = new Queue<T>();
    }
}