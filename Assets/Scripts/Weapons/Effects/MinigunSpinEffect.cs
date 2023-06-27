using UnityEngine;

[CreateAssetMenu(menuName = "Scripted Effects/Minigun Spin Effect")]
public class MinigunSpinEffect : ScriptedEffect<WeaponBase, float>
{
    public float speed;
    public float maxSpeed;

    public override void Init(WeaponBase controller)
    {
        controller.CustomProperties.Add(CustomWeaponProperty.BarrelVelocity, 0);
    }
    public override float Trigger(WeaponBase controller) 
    {
        float velocity = controller.CustomProperties[CustomWeaponProperty.BarrelVelocity];
        velocity = Mathf.Clamp(velocity + speed, 0, maxSpeed);

        controller.CustomProperties[CustomWeaponProperty.BarrelVelocity] = velocity;
        return velocity;
    }

    public override void UpdateEffect(WeaponBase controller) 
    {
        float velocity = controller.CustomProperties[CustomWeaponProperty.BarrelVelocity];
        controller.Barrel.Rotate(0, -velocity * Time.deltaTime, 0, Space.Self);

        velocity = Mathf.Clamp( velocity - speed * Time.deltaTime, 0, maxSpeed);
        controller.CustomProperties[CustomWeaponProperty.BarrelVelocity] = velocity;
    }
}
