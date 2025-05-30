using DG.Tweening;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [HideInInspector] public bool IsBulletFree = false;
    private float _damageAmount = 10f;
    private GameObject _target;
    private AmmunitionData _ammunitionData;

    public void ReInitializeBullet(GameObject target, AmmunitionData ammunitionData, float damageAmount)
    {
        _ammunitionData = ammunitionData;
        _target = target;
        _damageAmount = damageAmount;

        gameObject.SetActive(true);
        IsBulletFree = false;

        //_target.GetComponent<EntityController>().OnEntityDestroy += TargetIsDie;

        RunBullet();
    }

    private void RunBullet()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform.position);

            transform.DOMove(_target.transform.position, _ammunitionData.speed)
                .SetEase(Ease.Linear)
                .OnComplete(() => EndBulletRun())
                .SetAutoKill(true);
        }
    }

    private void EndBulletRun()
    {
        var _targetHealth = _target.GetComponent<IHealth>();

        _targetHealth.TakeDamage(_damageAmount);
        DisactivateBullet();
    }

    private void DisactivateBullet()
    {
        DOTween.Kill(transform);

        IsBulletFree = true;
        gameObject.SetActive(false);
    }

    //private void TargetIsDie(Entity entity)
    //{
    //    DisactivateBullet();
    //    entity.OnEntityDestroy -= TargetIsDie;
    //}
}
