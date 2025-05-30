using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponData weaponData;

    public WeaponData WeaponData { get { return weaponData; } }

    private IShootComponent _shootComponent;
    private IReloadComponent _reloadComponent;
    private ITargetFinder _targetFinder;

    public void Initialize()
    {
        _shootComponent = GetComponent<IShootComponent>();
        _reloadComponent = GetComponent<IReloadComponent>();
        _targetFinder = GetComponent<ITargetFinder>();

        _shootComponent.Initialize(weaponData);
        _targetFinder.Initialize(weaponData);

        StartCoroutine(AutomaticShoot());
    }

    public void Fire()
    {
        if (!_reloadComponent.IsWeaponReloaded || !_targetFinder.IsTargetFinded)
            return;

        if (!_targetFinder.IsTargetKillable)
            return;

        _shootComponent.Shoot(_targetFinder.GetTarget());
        //_reloadComponent.Reload();
    }

    private IEnumerator AutomaticShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponData.fireRate);
            Fire();
        }
    }
}
