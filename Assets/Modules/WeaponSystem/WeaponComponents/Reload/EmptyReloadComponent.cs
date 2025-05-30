using UnityEngine;

public class EmptyReloadComponent : MonoBehaviour, IReloadComponent
{
    public bool IsWeaponReloaded => true;

    public void Initialize(WeaponData weaponData)
    {

    }

    public void Reload()
    {

    }
}
