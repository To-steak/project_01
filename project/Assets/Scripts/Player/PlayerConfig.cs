using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float JumpForce = 10f;
    public Vector3 GroundCheckOffset;
    public float GroundDistance = 1;
    public LayerMask GroundLayer;
}
