using System.Collections.Generic;
using UnityEngine;



public class ObjectPool
{
    private readonly GameObject prefab;
    private readonly int initialCount = 10;

    private readonly Queue<GameObject> pool;

    public GameObject Prefab { get { return prefab; } }

    public ObjectPool()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < initialCount; i++) {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Spawn(Vector3 _position, Quaternion _rotation)
    {
        GameObject obj;
        if(pool.Count > 0) {
            obj = pool.Dequeue();
        } else {
            obj = GameObject.Instantiate(prefab);
        }
        obj.transform.position = _position;
        obj.transform.rotation = _rotation;
        return obj;
    }

    public void ReturnToPool(GameObject _obj)
    {
        _obj.SetActive(false);
        pool.Enqueue(_obj);
    }
}
