using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable] 
public enum DecalType 
{ 
    Bullethole_Concrete = 0,
    Bullethole_Metal,
    Bullethole_Wood,
    Blood
}

[Serializable]
public enum ParticleEffectType
{
    Smoke = 0,
    Concrete_Debris
}

[CreateAssetMenu(menuName = "Scriptable Objects/Data/Effects Library")]
public class EffectsLibrary : ScriptableObject
{
    [SerializeField] private List<Material> decalLibrary;
    [SerializeField] private List<PooledObject> particleLibrary;

    public Material FindDecalMaterial(DecalType _type)
    {
        Material m = decalLibrary[(int)_type];
        return m;
    }

    public PooledObject FindParticleEffect(ParticleEffectType _type)
    {
        PooledObject p = particleLibrary[(int)_type];
        return p;
    }

}
