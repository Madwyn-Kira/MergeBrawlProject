using UnityEngine;

public interface ITargetFinder
{
    public void Initialize(WeaponData weaponData);
    public GameObject GetTarget();
    public bool IsTargetFinded { get; }
    public bool IsTargetKillable { get; }
}
