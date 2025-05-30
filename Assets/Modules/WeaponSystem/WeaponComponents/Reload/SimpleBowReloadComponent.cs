using System.Collections;
using UnityEngine;

public class SimpleBowReloadComponent : MonoBehaviour, IReloadComponent
{
    private WeaponData _weaponData;
    private bool _isWeaponReloaded = true;

    public bool IsWeaponReloaded { get { return _isWeaponReloaded; } }

    public void Initialize(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public void Reload()
    {
        _isWeaponReloaded = false;
        WaitUntilReload();
    }

    private IEnumerator WaitUntilReload()
    {
        yield return new WaitForSeconds(_weaponData.reloadTime);
        _isWeaponReloaded = true;
    }
}
