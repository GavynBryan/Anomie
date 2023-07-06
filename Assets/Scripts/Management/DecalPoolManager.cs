using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering.Universal;

public partial class ObjectPoolManager
{
    private Queue<DecalProjector> decalPool;
    private Queue<DecalProjector> decalsInWorld;
    public DecalProjector SpawnDecal(Material _material, Vector2 _size, Vector3 _position, Vector3 _normal, Transform _transform)
    {
        DecalProjector decal = null;
        if (decalsInWorld.Count < Globals.MaxDecalCount) {
            if(decalPool.Count == 0) {
                AddToDecalPool();
            }
            decal = decalPool.Dequeue();
        } else {
            decal = decalsInWorld.Dequeue();
        }

        if (decal != null) {
            decal.gameObject.SetActive(true);
            decal.material = _material;
            decal.gameObject.transform.position = _position;
            decal.transform.forward = -_normal;
            decal.transform.parent = _transform;
            decal.size = new Vector3(_size.x, _size.y, 0.10f);
           
            decalsInWorld.Enqueue(decal);
        }
        return decal;
    }
    private void AddToDecalPool()
    {
        for(int i = 0; i < 8; i++) {
            var go = new GameObject("decal");
            var projector = go.AddComponent<DecalProjector>();
            
            projector.pivot = Vector3.zero;
            go.SetActive(false);

            decalPool.Enqueue(projector);
        }
    }
}
