using UnityEngine;

public interface ITargetFinder
{
    public void Initialize(WeaponData weaponData);
    public GameObject GetTarget();
    public void StartFindCoroutine();
    public void StopFindCoroutine();
    public bool IsTargetFinded { get; }
    public bool IsTargetKillable { get; }
}
