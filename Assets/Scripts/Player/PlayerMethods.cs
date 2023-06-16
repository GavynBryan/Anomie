using UnityEngine;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    public void OnLand()
    {
        if (velocity.y <= -20) {
            float ratio = (-velocity.y - 20) / 15;
            int damage = (int)Mathf.Lerp(5, 100, ratio);
            DamageInfo fallDamage = new DamageInfo(DamageType.IMPACT, damage, gameObject, Vector3.zero, Vector3.zero);
            TakeDamage(fallDamage);
        }
    }
}
