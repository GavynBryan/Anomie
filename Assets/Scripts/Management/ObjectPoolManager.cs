
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager 
{
    private HashSet<ObjectPool> objectPools;

    public GameObject SpawnFromPool(GameObject _obj)
    {
        ObjectPool pool = objectPools.Where(x => x.Prefab == _obj).FirstOrDefault();
        
}
