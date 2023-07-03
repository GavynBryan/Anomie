using UnityEngine;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    public void OnLand()
    {
        if (velocity.y <= -20) {
            float ratio = (-velocity.y - 20) / 15;
            int damage = (int)Mathf.Lerp(5, 100, ratio);
            DamageInfo fallDamage = new DamageInfo(DamageType.IMPACT, damage, gameObject, Vector3.zero);
            TakeDamage(fallDamage, transform.position);
        }
    }

    public void Use()
    {
        RaycastHit hit;
        int mask = 1 << 7;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red, 2f);
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2.5f, mask, QueryTriggerInteraction.Ignore)) 
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            interactable?.OnInteract(this);
        }
    }

    public void PickupObject(PhysicsProp prop)
    {
        stateController.SwitchState(new PlayerHoldingState(this, prop));
    }
}
