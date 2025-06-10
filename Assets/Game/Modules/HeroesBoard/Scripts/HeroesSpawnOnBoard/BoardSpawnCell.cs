using System;
using UnityEngine;

[Serializable]
public class BoardSpawnCell : MonoBehaviour
{
    [SerializeField] private Transform placementPoint;

    [SerializeField] public Entity CurrentHero { get; private set; }
    [SerializeField] public bool IsEmpty => CurrentHero == null;

    [SerializeField] public IBoard Board;

    private void Awake()
    {
        Board = transform.parent.GetComponent<IBoard>();
    }

    public void AssignHero(Entity entity)
    {
        CurrentHero = entity;

        if (entity.CurrentCell != null)
            Board.UnitMoveCell(entity, entity.CurrentCell, this);

        entity.AssignCell(this);

        entity.transform.SetParent(transform);
        entity.transform.localScale = CurrentHero.ConfigSettings.ScaleForSpawn;

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
