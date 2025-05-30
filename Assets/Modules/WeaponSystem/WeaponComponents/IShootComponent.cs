using UnityEngine;

public interface IShootComponent
{
    public void Initialize(WeaponData data);
    public void Shoot(GameObject target);
}