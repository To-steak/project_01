using UnityEngine;

public class FirearmsInstance : WeaponInstance
{
    public int CurrentAmmo;
    public int ReserveAmmo;

    private GameObject _bulletPrefab;
    private Muzzle _muzzle;

    public FirearmsInstance(GameObject weaponPrefab, GameObject bulletPrefab, Muzzle muzzle, int currentAmmo, int reserveAmmo) : base(weaponPrefab)
    {
        CurrentAmmo = currentAmmo;
        ReserveAmmo = reserveAmmo;
        _muzzle = muzzle;
        _bulletPrefab = bulletPrefab;
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Shot;
    }

    public override void Attack(Vector3 targetPosition)
    {
        Vector3 spawnPosition = _muzzle.transform.position;
        targetPosition.y = spawnPosition.y;
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Object.Instantiate(_bulletPrefab, spawnPosition, rotation);
    }
}