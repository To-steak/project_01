using UnityEngine;

public class FirearmsInstance : WeaponInstance
{
    public struct Config
    {
        public GameObject WeaponGameObject;
        public GameObject BulletPrefab;
        public Muzzle Muzzle;
        public float AttackSpeed;
        public int MaxAmmo;
        public int ReserveAmmo;
    }

    private GameObject _bulletPrefab;
    private Muzzle _muzzle;
    private int _maxAmmo;
    private int _currentAmmo;
    private int _reserveAmmo;

    public FirearmsInstance(Config config) : base(config.WeaponGameObject, config.AttackSpeed)
    {
        _bulletPrefab = config.BulletPrefab;
        _muzzle = config.Muzzle;
        _maxAmmo = config.MaxAmmo;
        _currentAmmo = _maxAmmo;
        _reserveAmmo = config.ReserveAmmo;
    }

    public override bool Attack(Vector3 position, Transform transform)
    {
        if (_currentAmmo <= 0)
        {
            return false;
        }

        _currentAmmo--;

        Vector3 spawnPosition = _muzzle.transform.position;
        position.y = spawnPosition.y;
        Vector3 direction = (position - spawnPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = ObjectPool.Pool.Get(_bulletPrefab, spawnPosition, rotation);

        if (bullet.TryGetComponent<Bullet>(out var component))
        {
            component.SetShooter(transform);
        }
        
        return true;
    }

    public override bool IsReloadableWeapon()
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