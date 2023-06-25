﻿using UnityEngine;

public enum WeaponState
{
    IDLE = 0,
    SWITCHING,
    HOLSTERED
}

public partial class PlayerController : MonoBehaviour, IDamageable
{
    private Stat health;
    private WeaponState weaponState;
    private WeaponBase currentWeapon;

    public Stat Health { get { return health; } }
    public WeaponState WeaponState { get { return weaponState; } }
    public WeaponBase CurrentWeapon { get { return currentWeapon; } }

    public void TakeDamage(DamageInfo damageInfo)
    {
        health -= damageInfo.Damage;
    }

    public void FireWeapon()
    {
        if (weaponState == WeaponState.IDLE) {
            currentWeapon.Fire();
        }
    }

    public void FireWeaponHold()
    {
        if(weaponState == WeaponState.IDLE) {
            currentWeapon.FireContinuously();
        }
    }
}
