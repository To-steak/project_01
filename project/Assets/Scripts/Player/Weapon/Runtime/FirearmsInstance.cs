using UnityEngine;

public class FirearmsInstance : WeaponInstance
{
    public struct Config
    {
        public GameObject WeaponPrefab;
        public GameObject BulletPrefab;
        public Muzzle Muzzle;
        public float AttackSpeed;
        public int MaxAmmo;
        public int ReserveAmmo;
    }

    public override float AttackSpeed => _attackSpeed;

    private GameObject _bulletPrefab;
    private Muzzle _muzzle;
    private float _attackSpeed;
    private int _maxAmmo;
    private int _currentAmmo;
    private int _reserveAmmo;

    public FirearmsInstance(Config config) : base(config.WeaponPrefab)
    {
        _bulletPrefab = config.BulletPrefab;
        _muzzle = config.Muzzle;
        _attackSpeed = config.AttackSpeed;
        _maxAmmo = config.MaxAmmo;
        _currentAmmo = _maxAmmo;
        _reserveAmmo = config.ReserveAmmo;
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Shot;
    }

    public override bool Attack(Vector3 targetPosition)
    {
        if (_currentAmmo <= 0)
        {
            return false;
        }

        _currentAmmo--;

        Vector3 spawnPosition = _muzzle.transform.position;
        targetPosition.y = spawnPosition.y;
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Object.Instantiate(_bulletPrefab, spawnPosition, rotation);

        return true;
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