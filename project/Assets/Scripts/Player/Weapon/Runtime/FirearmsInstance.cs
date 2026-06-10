using UnityEngine;

public class FirearmsInstance : WeaponInstance
{
    private GameObject _bulletPrefab;
    private Muzzle _muzzle;
    private int _maxAmmo;
    private int _currentAmmo;
    private int _reserveAmmo;

    public FirearmsInstance(GameObject weaponPrefab, GameObject bulletPrefab, Muzzle muzzle, int maxAmmo, int reserveAmmo) : base(weaponPrefab)
    {
        _maxAmmo = maxAmmo;
        _currentAmmo = _maxAmmo;
        _reserveAmmo = reserveAmmo;
        _muzzle = muzzle;
        _bulletPrefab = bulletPrefab;
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Shot;
    }

    public override void Attack(Vector3 targetPosition)
    {
        _currentAmmo--;

        Vector3 spawnPosition = _muzzle.transform.position;
        targetPosition.y = spawnPosition.y;
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Object.Instantiate(_bulletPrefab, spawnPosition, rotation);
    }

    public override bool TryReload()
    {
        return _currentAmmo < _maxAmmo && _reserveAmmo > 0;
    }

    public override void Reload()
    {
        int need = _maxAmmo - _currentAmmo;
        int reload = Mathf.Min(need, _reserveAmmo);
        _currentAmmo += reload;
        _reserveAmmo -= reload;
        Debug.Log($"Reload: {reload}발 장전 / 현재 {_currentAmmo}/{_maxAmmo} | 예비 {_reserveAmmo}");
    }
}