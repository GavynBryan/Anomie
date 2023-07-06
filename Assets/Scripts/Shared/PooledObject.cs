using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Base class for objects to return to an object pool when disabled
/// </summary>
public class PooledObject : MonoBehaviour
{

    ObjectPool<PooledObject> pool;

    public void AssignPool(ObjectPool<PooledObject> _pool)
    {
        pool = _pool;
    }

    void OnDisable()
    {
        pool?.Release(this);
    }
}
