using System;
using UnityEngine;

/// <summary>
/// An interface that is required for PoolObject
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPoolable<T>
{
    /// <summary>
    /// Invoke this event to return to the pool
    /// </summary>
    Action<T> OnRelease { get; set; }
    GameObject gameObject { get; }
}

public interface IPoolable
{
    void Release();
}
