using UnityEngine;

public class HandAttackComponent : MonoBehaviour, IShootComponent
{
    private WeaponData _weaponData;

    public void Initialize(WeaponData data)
    {
        _weaponData = data;
    }

    public void Shoot(GameObject target)
    {
        if (target == null)
            return;

        target.GetComponent<IHealth>().TakeDamage(_weaponData.damage);
    }
}
