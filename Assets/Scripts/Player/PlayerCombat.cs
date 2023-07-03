using UnityEngine;

public enum WeaponState
{
    IDLE = 0,
    SWITCHING,
    HOLSTERED
}

public partial class PlayerController : MonoBehaviour, IDamageable
{
    private Stat health;
    private WeaponState weaponState = WeaponState.IDLE;
    private Transform weaponHolder;
    private WeaponBase currentWeapon;

    public Stat Health { get { return health; } }
    public WeaponState WeaponState { get { return weaponState; } }
    public Transform WeaponHolder { get { return weaponHolder; } }
    public WeaponBase CurrentWeapon { get { return currentWeapon; } }

    public void TakeDamage(DamageInfo _info, Vector3 _position)
    {
        health -= _info.Damage;
    }

    public void FireWeapon()
    {
        if (weaponState == WeaponState.IDLE) {
            currentWeapon.Fire(false);
        }
    }

    public void FireWeaponHold()
    {
        if(weaponState == WeaponState.IDLE) {
            currentWeapon.Fire(true);
        }
    }
}
