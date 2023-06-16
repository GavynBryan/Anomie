using UnityEngine;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    private Stat health;

    public Stat Health { get { return health; } }

    public void TakeDamage(DamageInfo damageInfo)
    {
        health -= damageInfo.Damage;
    }
}
