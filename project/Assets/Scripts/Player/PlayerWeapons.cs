using UnityEngine;

public class PlayerWeapons : MonoBehaviour, IPlayerWeapon
{
    public float AttackSpeed => instances[_currentWeaponIndex].AttackSpeed;

    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform hand;

    private WeaponInstance[] instances;
    private int _currentWeaponIndex;

    public void Initialize()
    {
        var weaponCount = weapons.Length;
        instances = new WeaponInstance[weaponCount];
        _currentWeaponIndex = 0;

        for (int i = 0; i < weaponCount; i++)
        {
            instances[i] = weapons[i].Initialize(hand: hand);
        }

        instances[_currentWeaponIndex].WeaponGameObject.SetActive(true);
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return (instances[_currentWeaponIndex] as IPlayerWeapon).GetAttackState(controller);
    }

    public bool TrySwapWeapon(int index)
    {
        if (index == _currentWeaponIndex)
        {
            return false;
        }

        if (instances[index] == null)
        {
            return false;
        }

        var before = instances[_currentWeaponIndex];
        before.WeaponGameObject.SetActive(false);

        _currentWeaponIndex = index;

        var after = instances[_currentWeaponIndex];
        after.WeaponGameObject.SetActive(true);

        return true;
    }

    public bool TryAttack(Vector3 position)
    {
        return instances[_currentWeaponIndex].Attack(position, transform);
    }

    public bool IsReloadableWeapon()
    {
        return instances[_currentWeaponIndex].IsReloadableWeapon();
    }

    public void Reload()
    {
        instances[_currentWeaponIndex].Reload();
    }

#if UNITY_EDITOR
    private Vector3 _position = Vector3.zero;

    public void MeleeArea(Vector3 position)
    {
        _position = position;
    }

    private void OnDrawGizmos()
    {
        if (instances == null || instances[_currentWeaponIndex] is not MeleeInstance meleeWeapon)
            return;

        Vector3 direction = (_position - transform.position).normalized;
        Vector3 center = transform.position + (direction * 1.0f);
        Quaternion rotation = Quaternion.LookRotation(direction);

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, meleeWeapon.HalfExtents * 2);
    }
#endif
}
