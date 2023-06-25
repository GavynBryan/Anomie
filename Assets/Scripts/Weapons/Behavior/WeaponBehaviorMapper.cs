using UnityEngine;

public class WeaponBehaviorMapper
{
    private readonly Transform origin;
    private readonly GameObject owner;

    public Transform Origin { get { return origin; } }
    public GameObject Owner { get { return owner; } }
    public WeaponBehaviorMapper(Transform _origin, GameObject _owner)
    {
        origin = _origin; 
        owner = _owner;
    }
}
