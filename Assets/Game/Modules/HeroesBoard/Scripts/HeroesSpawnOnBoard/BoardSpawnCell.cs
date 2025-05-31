using UnityEngine;

public class BoardSpawnCell : MonoBehaviour
{
    [SerializeField] private Transform placementPoint;

    public Entity CurrentHero { get; private set; }
    public bool IsEmpty => CurrentHero == null;

    public void AssignHero(Entity entity)
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

    public bool CanAccept(HeroController entity)
    {
        var _current = CurrentHero as HeroController;

        return IsEmpty || (_current != null && _current.CanMergeWith(entity));
    }
}
