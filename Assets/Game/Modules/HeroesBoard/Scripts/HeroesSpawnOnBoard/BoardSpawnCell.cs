using UnityEngine;

public class BoardSpawnCell : MonoBehaviour
{
    [SerializeField] private Transform placementPoint;

    public HeroController CurrentHero { get; private set; }
    public bool IsEmpty => CurrentHero == null;

    public void AssignHero(HeroController entity)
    {
        CurrentHero = entity;
        entity.AssignCell(this);

        entity.transform.SetParent(transform);
        entity.transform.localScale = new Vector3(1, 1, 1);

        entity.transform.localPosition = placementPoint.localPosition;
    }

    public void Clear()
    {
        CurrentHero = null;
    }

    public bool CanAccept(HeroController entity) =>
        IsEmpty || (CurrentHero != null && CurrentHero.CanMergeWith(entity));
}
