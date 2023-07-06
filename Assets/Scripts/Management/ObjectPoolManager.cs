using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

public partial class ObjectPoolManager 
{
    private Dictionary<PooledObject, ObjectPool<PooledObject>> objectPools;

    private static readonly ObjectPoolManager instance = new ObjectPoolManager();
    public static ObjectPoolManager Instance
    {
        get { return instance; }
    }

    private ObjectPoolManager()
    {
        objectPools = new Dictionary<PooledObject, ObjectPool<PooledObject>>();

        decalPool = new Queue<DecalProjector>();
        decalsInWorld = new Queue<DecalProjector>();
    }

    /// <summary>
    /// Returns an instance of the prefab
    /// </summary>
    /// <param name="_prefab">The prefab to copy</param>
    /// <returns></returns>
    public PooledObject SpawnFromPool(PooledObject _prefab)
    {
        ObjectPool<PooledObject> pool;

        if (objectPools.ContainsKey(_prefab)) { 
            pool = objectPools[(_prefab)];
        } else{
            pool = CreateObjectPool(_prefab);
        }
        return pool.Get();
    }

    /// <summary>
    /// Returns an instance of the prefab spawned at the given position
    /// </summary>
    /// <param name="_prefab">The prefab to copy</param>
    /// <param name="_position">The position to spawn the prefab</param>
    /// <returns></returns>
    public PooledObject SpawnFromPool(PooledObject _prefab, Vector3 _position)
    {
        PooledObject obj = SpawnFromPool(_prefab);
        obj.transform.position = _position;
        return obj;
    }

    /// <summary>
    /// Returns an instance of the prefab spawn at the given position with the given rotation
    /// </summary>
    /// <param name="_prefab">The prefab to copy</param>
    /// <param name="_position">The position to spawn the prefab</param>
    /// <param name="_rotation">The rotation to spawn the prefab</param>
    /// <returns></returns>
    public PooledObject SpawnFromPool(PooledObject _prefab, Vector3 _position, Quaternion _rotation)
    {
        PooledObject obj = SpawnFromPool(_prefab);
        obj.transform.position = _position;
        obj.transform.rotation = _rotation;
        return obj;
    }

    private ObjectPool<PooledObject> CreateObjectPool(PooledObject _prefab)
    {
        PooledObject OnInstantiate()
        {
            var go = GameObject.Instantiate(_prefab);
            var pool = objectPools[_prefab];
            go.AssignPool(pool);
            return go;
        }
        ObjectPool<PooledObject> pool = new ObjectPool<PooledObject>(OnInstantiate, OnTakenFromPool, 
                                                                    OnReturnedToPool, OnDestroyPoolObject, true, 15, 200);
        objectPools.Add(_prefab, pool);
        return pool;
    }
    
    private void OnReturnedToPool(PooledObject _instance)
    {
        _instance.gameObject.SetActive(false);
    }

    private void OnTakenFromPool(PooledObject _instance)
    {
        _instance.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PooledObject _instance)
    {
        GameObject.Destroy(_instance);
    }
}
