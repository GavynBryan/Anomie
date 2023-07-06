using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;

public class EffectsManager
{
    private readonly EffectsLibrary library;
    
    public EffectsManager(EffectsLibrary _library) 
    { 
        library = _library;
    }

    /// <summary>
    /// Produces a decal at a raycast hit point that matches the provided material
    /// </summary>
    /// <param name="_material">Material that was hit</param>
    /// <param name="hit">The raycast hit of the bullet</param>
    public void CreateBulletDecal(Material _material, RaycastHit hit)
    {
        var decalMaterial = library.FindDecalMaterial(DecalType.Bullethole_Concrete);
        if (_material.HasFloat("_MATERIAL_TYPE")) {
            switch ((MaterialProperty)_material.GetFloat("_MATERIAL_TYPE")) {
                //Concrete is default so we can skip it
                case MaterialProperty.Metal:
                    decalMaterial = library.FindDecalMaterial(DecalType.Bullethole_Metal);
                    break;
                case MaterialProperty.Wood:
                    decalMaterial = library.FindDecalMaterial(DecalType.Bullethole_Wood);
                    break;
            }
        }
        if (decalMaterial != null) {
            ObjectPoolManager.Instance.SpawnDecal(decalMaterial, new Vector2(.25f, .25f), hit.point, hit.normal, hit.transform);
        }
    }

    /// <summary>
    /// Produces particle effects based on the provided material
    /// </summary>
    /// <param name="_material">Material that was hit</param>
    /// <param name="_position">The position of the particle to be spawned</param>
    /// <param name="_rotation">The rotation of the particle to be spawned</param>
    public void CreateImpactParticleEffects(Material _material, Vector3 _position, Quaternion _rotation)
    {
        List<PooledObject> prefabsToSpawn = new List<PooledObject>();
        if (_material.HasFloat("_MATERIAL_TYPE")) {
            switch((MaterialProperty)_material.GetFloat("_MATERIAL_TYPE")) {
                case MaterialProperty.Concrete:
                    prefabsToSpawn.Add(library.FindParticleEffect(ParticleEffectType.Smoke));
                    break;
            }
        }
        for (int i = 0; i < prefabsToSpawn.Count; i++) {
            if (prefabsToSpawn[i] != null) {
                ObjectPoolManager.Instance.SpawnFromPool(prefabsToSpawn[i], _position, _rotation);
            }
        }
    }

    /// <summary>
    /// Produces particle effect at the given position with the given rotation
    /// </summary>
    /// <param name="_particle">The type of particle to be spawned</param>
    /// <param name="_position">The position of the particle to be spawned</param>
    /// <param name="_rotation">The rotation of the particle to be spawned</param>
    public void CreateParticleEffect(ParticleEffectType _particle, Vector3 _position, Quaternion _rotation)
    {
        var particlePrefab = library.FindParticleEffect(_particle);
        if (particlePrefab != null) {
            ObjectPoolManager.Instance.SpawnFromPool(particlePrefab, _position, _rotation);
        }
    }
}
