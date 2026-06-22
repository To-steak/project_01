using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy Agent")]
    public LayerMask PlayerLayer;
    public LayerMask ObstacleLayer;
    public float WalkSpeed;
    public float RunSpeed;
    public float AttackRange;
    public float AttackInterval;
    public float NextMoveInterval;
    public float MinMoveRadius;
    public float MaxDetectRadius;
    public float AbsoluteDetectRadius;
    public float RotationSpeed;
    [Header("Enemy Animation")]
    public float WalkAnimSpeed;
    public float RunAnimSpeed;
    public float AttackSpeed;
    [Header("Enemy Health")]
    public float InitHealth;
}
