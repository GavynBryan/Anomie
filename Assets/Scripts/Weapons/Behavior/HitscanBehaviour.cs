using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Behaviours/Hitscan Behavior")]
public class HitscanBehaviour : SimpleBehaviour<WeaponBehaviorMapper>
{
    #region Properties
    public float spread;
    public int damage;
    public int bulletsPerFire = 1;
    public float distance = 5;
    public float bulletWidth = 0.1f;
    #endregion

    public override void Execute(WeaponBehaviorMapper mapper)
    {
        Vector3 origin = mapper.Origin.position;
        Vector3 fwd = mapper.Origin.transform.forward;
        DamageInfo damageInfo = new DamageInfo(DamageType.BULLET, damage, mapper.Owner, origin, fwd);

        for (int i = 0; i < bulletsPerFire; i++) {
            Vector3 angle = GetSpreadAngle(fwd);
            RaycastHit hit;
            if (Physics.SphereCast(origin, bulletWidth, angle, out hit, distance, 1, QueryTriggerInteraction.Ignore)) {
                IDamageable damageableObject = hit.transform.GetComponent<IDamageable>();
                damageableObject?.TakeDamage(damageInfo);
            }
        }
    }

    public Vector3 GetSpreadAngle(Vector3 fwd)
    {
        float ratio = spread / 180;
        Vector3 dir = Vector3.Slerp(fwd, Random.insideUnitSphere, ratio);
        return dir;
    }
}
