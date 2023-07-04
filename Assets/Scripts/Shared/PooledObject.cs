using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{

    ObjectPool<PooledObject> pool;

    public void AssignPool(ObjectPool<PooledObject> _pool)
    {
        pool = _pool;
    }

    void OnDisable()
    {
        pool.Release(this);
    }
}
