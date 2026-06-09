using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Movements")]
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float DodgeSpeed = 15f;
    public float JumpForce = 10f;
    public float Gravity = -9.81f;
    public Vector3 GroundCheckOffset = Vector3.zero;
    public float GroundDistance = 1;
    public LayerMask GroundLayer;
}
