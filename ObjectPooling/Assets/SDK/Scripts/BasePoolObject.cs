using System;
using UnityEngine;

public abstract class BasePoolObject<T> : MonoBehaviour, IPoolable, IPoolable<T> where T : class 
{
    public Action<T> OnRelease { get; set; }

    public void Release()
    {
        if (OnRelease != null)
        {
            var instance = this as T;
            OnRelease.Invoke(instance);
        }
    }
}