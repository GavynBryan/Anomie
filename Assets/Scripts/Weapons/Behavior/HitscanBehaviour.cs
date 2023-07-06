using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon Behaviours/Hitscan Behavior")]
public class HitscanBehaviour : WeaponBehavior<WeaponBehaviorMapper>
{
    #region Properties
    public float spread;
    public int damage;
    public int bulletsPerFire = 1;
    public float distance = 5;
    public float bulletWidth = 0.1f;
    #endregion

    public override void Execute(WeaponBehaviorMapper _mapper, int _shots)
    {
        Vector3 origin = _mapper.Origin.position;
        Vector3 fwd = _mapper.Origin.transform.forward;
        DamageInfo damageInfo = new DamageInfo(DamageType.BULLET, damage, _mapper.Owner, fwd);

        for (int i = 0; i < bulletsPerFire * _shots; i++) {
            Vector3 angle = GetSpreadAngle(fwd);
            int mask = 1 | (1 << 7);
            RaycastHit hit;
            if (Physics.SphereCast(origin, bulletWidth, angle, out hit, distance, mask, QueryTriggerInteraction.Ignore)) {
                IDamageable damageableObject = hit.transform.GetComponent<IDamageable>();
                damageableObject?.TakeDamage(damageInfo, hit.point);

                var mat = hit.GetMaterial();
                if (mat != null) {
                    GameManager.Effects.CreateBulletDecal(mat, hit);
                }
                GameManager.Effects.CreateImpactParticleEffects(mat, hit.point, Quaternion.LookRotation(hit.normal));

            }
        }
    }

    public Vector3 GetSpreadAngle(Vector3 _fwd)
    {
        float ratio = spread / 180;
        Vector3 dir = Vector3.Slerp(_fwd, Random.insideUnitSphere, ratio);
        return dir;
    }


}
