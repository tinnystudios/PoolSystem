using System.Collections.Generic;
using UnityEngine;

namespace Liminal.ObjectPooling
{
    /// <summary>
    /// A shared factory for T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class PoolSharedFactory<T> where T : Object, IPoolable<T>
    {
        private static Dictionary<T, Pool<T>> _lookUp = new Dictionary<T, Pool<T>>();

        public static int Count { get { return _lookUp.Count; } }

        // Returns a cached pool or cache a new one
        public static Pool<T> GetOrCreatePool(T prefab)
        {
            if (!_lookUp.ContainsKey(prefab))
            {
                var pool = new Pool<T>(prefab);
                _lookUp.Add(prefab, pool);
            }

            return _lookUp[prefab];
        }
        
        // Returns the object from the pool it belongs to
        public static T GetObject(T prefab, Vector3? position = null, Quaternion? rotation = null, Transform parent = null, bool active = true)
        {
            var pool = GetOrCreatePool(prefab);
            return pool.Get(position, rotation, parent, active);
        }
    }
}