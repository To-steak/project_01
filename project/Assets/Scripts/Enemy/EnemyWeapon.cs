using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weapon;

    private WeaponInstance instance;

    public void Initialize()
    {
        instance = weapon.Initialize(hand: null);

    }

    public bool TryAttack(Vector3 position)
    {
        return instance.Attack(position, transform);
    }

#if UNITY_EDITOR
    private Vector3 _position = Vector3.zero;

    public void MeleeArea(Vector3 position)
    {
        _position = position;
    }

    private void OnDrawGizmos()
    {
        if (instance == null || instance is not MeleeInstance meleeWeapon)
            return;

        // 1. 여기서 사용하는 방향은 Y값이 제거된 정규화된 벡터여야 합니다.
        Vector3 direction = (_position - transform.position).normalized;
        direction.y = 0;
        direction.Normalize();

        // 2. 캐릭터의 전체 회전을 쓰지 말고, 방향에서 만들어진 회전을 사용
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Gizmos.color = Color.red;
        // 3. 만들어진 회전을 행렬에 대입
        Gizmos.matrix = Matrix4x4.TRS(_position, lookRotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, meleeWeapon.HalfExtents * 2);
    }
#endif
}