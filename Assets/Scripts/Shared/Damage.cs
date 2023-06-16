using UnityEngine;

public enum DamageType
{
    BULLET,
    EXPLOSIVE,
    MELEE,
    ELECTRIC,
    IMPACT
}
public class DamageInfo
{
    private readonly DamageType damageType;
    private readonly int damage;
    private readonly GameObject offender;
    private readonly Vector3 origin;
    private readonly Vector3 direction;

    public DamageType DamageType { get { return damageType; } }
    public int Damage { get { return damage; } }
    public GameObject Offender { get { return offender; } }
    public Vector3 Origin { get { return origin; } }
    public Vector3 Direction { get { return direction; } }  
    
    public DamageInfo(DamageType _damageType, int _damage, GameObject _offender, Vector3 _origin, Vector3 _direction)
    { 
        damageType = _damageType;
        damage = _damage;
        offender = _offender;
        origin = _origin;
        direction = _direction;
    }
}

interface IDamageable
{
    void TakeDamage(DamageInfo _info);
}