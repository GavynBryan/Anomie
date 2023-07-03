using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    #region Properties
    [SerializeField] protected float fireRate;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected float recoil;
    [SerializeField] protected bool continuous;
    [SerializeField] protected Transform barrel;
    [SerializeField] protected WeaponBehavior<WeaponBehaviorMapper> behavior;
    [SerializeField] protected ScriptedEffect<WeaponBase, float> effect;
    [SerializeField] protected AudioClip shotSound;
    [SerializeField] protected AudioSource audioSource;

    protected Transform origin;
    protected Dictionary<CustomWeaponProperty, float> customProperties;

    private float nextFireTime;
    private WeaponBehaviorMapper mapper;

    public float FireRate { get { return fireRate; } }
    public Sprite Icon { get { return icon; } }
    public float Recoil { get { return recoil; } }
    public bool Continuous { get { return continuous; } }
    public Transform Barrel { get { return barrel; } }
    public Transform Origin { get { return origin; } }
    public Dictionary<CustomWeaponProperty, float> CustomProperties { get { return customProperties; } }

    #endregion
    public WeaponBase()
    {
        customProperties = new Dictionary<CustomWeaponProperty, float>();
    }

    void Start()
    {
        effect?.Init(this);    
    }

    private void Update()
    {
        effect?.UpdateEffect(this);
    }

    public void AssignToPlayer(PlayerController player)
    {
        mapper = new WeaponBehaviorMapper(player.PlayerCamera.transform, gameObject);
    }

    public virtual bool Fire(bool holdingTrigger)
    {
        if (nextFireTime < Time.time) {

            if (!holdingTrigger) {
                nextFireTime = Time.time;
            }

            //Compensate for lag on lower end machines
            int shots = 0;
            while(nextFireTime <=  Time.time) {
                audioSource.PlayOneShot(shotSound);
                nextFireTime += fireRate;
                shots++;
            }
            behavior.Execute(mapper, shots);
            effect?.Trigger(this);
            return true;
        }
        return false;
    }
}
