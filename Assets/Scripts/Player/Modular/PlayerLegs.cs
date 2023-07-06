using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Leg Modules/Default")]
public class PlayerLegs : ScriptableObject
{
    public float moveSpeed = 5;
    public float jumpForce = 12;
    public float airFriction = 0.75f;
    public float gravityMultiplier = 3;
    public float groundFriction = 3.25f;
    public float slideAngle = 35;

    public int maxJumpCombo = 0;
}
