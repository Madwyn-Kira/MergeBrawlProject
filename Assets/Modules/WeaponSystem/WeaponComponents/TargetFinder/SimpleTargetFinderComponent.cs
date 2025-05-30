using System.Collections;
using UnityEngine;

public class SimpleTargetFinderComponent : MonoBehaviour, ITargetFinder
{
    [SerializeField]
    private LayerMask targetLayer;

    private GameObject _target;
    private WeaponData _weaponData;

    public bool IsTargetFinded { get { return _target != null; } }
    public bool IsTargetKillable { get { return !_target.GetComponent<IHealth>().IsDead; } }

    public GameObject GetTarget() => _target;

    public void Initialize(WeaponData weaponData)
    {
        _weaponData = weaponData;

        StartCoroutine(FindTargetCoroutine());
    }

    public GameObject FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _weaponData.range, targetLayer);

        float closestDistance = Mathf.Infinity;
        GameObject target = null;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = collider.gameObject;
            }
        }

        return target;
    }

    private IEnumerator FindTargetCoroutine()
    {
        while (true)
        {
            _target = FindTarget();
            yield return new WaitForSeconds(1);
        }
    }
}
