using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour, IDamageable
{
    Color nextColor = Color.red;
    public void TakeDamage(DamageInfo damageInfo)
    {
        gameObject.GetComponent<Renderer>().material.color = nextColor;
        if (nextColor == Color.red) {
            nextColor = Color.blue;
        } else { nextColor = Color.red; }
    }
}
