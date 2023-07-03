using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsProp : MonoBehaviour, IInteractable, IDamageable
{
    private Rigidbody rb;
    private bool beingHeld;

    private HashSet<Rigidbody> currentCollisions = new HashSet<Rigidbody>();
    public bool BeingHeld { get { return beingHeld; } }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnInteract(PlayerController _player) 
    {
        if (_player.CurrentGround.transform != transform) {
            _player.PickupObject(this);
        }
    }

    public void onPickup()
    {
        beingHeld = true;

        DisableConstraints();
    }

    public void Drop()
    {
        beingHeld = false;

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10);
        EnableConstraints();
    }

    public void Throw(Vector3 _direction)
    {
        beingHeld = false;
        rb.AddForce(_direction, ForceMode.Impulse);
        EnableConstraints();
    }

    public void TakeDamage(DamageInfo _info, Vector3 _position)
    {
        if (!BeingHeld) {
            float force = _info.Damage;
            rb.AddForceAtPosition(_info.Direction * force, _position, ForceMode.Impulse);
        }
    }
    public void EnableConstraints()
    {
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.interpolation = RigidbodyInterpolation.None;
    }

    public void DisableConstraints()
    {
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController PC = collision.collider.GetComponent<PlayerController>();
        if (PC != null && beingHeld) {
            Drop();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null) {
            currentCollisions.Add(collision.rigidbody);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null) {
            currentCollisions.Remove(collision.rigidbody);
        }
    }

    public bool IsContactingHeavyObject
    {
        get
        {
            foreach (var collision in currentCollisions) {
                if (collision.mass > rb.mass) {
                    return true;
                }
            }
            return false;
        }
    }

}
