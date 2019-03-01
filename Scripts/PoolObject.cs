using UnityEngine;

namespace Liminal.ObjectPooling
{
    /// <summary>
    /// A base class for any pool object expecting a class that implements IPoolable allowing it to be released
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PoolObject<T> : MonoBehaviour where T : Object, IPoolable<T>
    {
        public Pool<T> Pool;

        [SerializeField] protected T _Prefab;
        [SerializeField] protected int _PoolSize = 10;
        [SerializeField] protected int _GrowAmount = 0;

        protected virtual void Awake()
        {
            Pool = new Pool<T>(_Prefab, _PoolSize, _GrowAmount, transform);
        }

        public T Get(Vector3? position = null, Quaternion? rotation = null, Transform parent = null, bool active = true)
        {
            return Pool.Get(position, rotation, parent, active);
        }
    }
}