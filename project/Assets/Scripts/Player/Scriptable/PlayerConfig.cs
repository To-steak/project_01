using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Movements")]
    public float WalkSpeed;
    public float RunSpeed;
    public float DodgeSpeed;
    public float JumpForce;
    public float Gravity;
    public Vector3 GroundCheckOffset = Vector3.zero;
    public float GroundDistance;
    public LayerMask GroundLayer;
    [Header("Player Health")]
    public float InitHealth;
    public float InitMana;
    public float RecoveryManaDelay;
    public float RecoveryManaAmount;
    public float RunCost;
    public float DodgeCost;

}
