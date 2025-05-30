using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleShooterComponent : MonoBehaviour, IShootComponent
{
    [SerializeField]
    private Transform ShootPosition;

    private WeaponData _weaponData;
    private List<GameObject> _bulletsPool = new();

    public void Initialize(WeaponData data)
    {
        _weaponData = data;
    }

    public void Shoot(GameObject target)
    {
        GameObject _freeBullet = _bulletsPool.Where(item => item.GetComponent<DamageDealer>().IsBulletFree).FirstOrDefault();

        if (_freeBullet != null)
        {
            var _bullet = _freeBullet.GetComponent<DamageDealer>();

            _bullet.transform.position = ShootPosition.position;
            _bullet.ReInitializeBullet(target, _weaponData.ammunition, _weaponData.damage);
        }
        else
        {
            var _bullet = Instantiate(_weaponData.ammunition.projectilePrefab, ShootPosition.position, Quaternion.identity);
            _bullet.GetComponent<DamageDealer>().ReInitializeBullet(target, _weaponData.ammunition, _weaponData.damage);

            _bulletsPool.Add(_bullet);
        }
    }
}
