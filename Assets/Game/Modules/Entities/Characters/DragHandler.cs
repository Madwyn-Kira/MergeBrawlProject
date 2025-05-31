using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;
    private Vector3 originalPosition;
    private HeroController entity;

    private float fixedHeight = 0f;
    private Vector3 dragOffset;
    private Plane dragPlane;

    private void Awake()
    {
        mainCamera = Camera.main;
        entity = GetComponent<HeroController>();
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedHeight, 0));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (dragPlane.Raycast(ray, out float enter))
        {
            originalPosition = transform.position;
            entity.CurrentCell?.Clear();
            Vector3 hitPoint = ray.GetPoint(enter);
            dragOffset = transform.position - hitPoint;
        }
        else
        {
            dragOffset = Vector3.zero;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 targetPosition = hitPoint + dragOffset;

            transform.position = new Vector3(targetPosition.x, fixedHeight, targetPosition.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out var hit, 10000000f, LayerMask.GetMask("BoardCell")))
        {
            var cell = hit.collider.GetComponent<BoardSpawnCell>();
            var targetUnit = cell.CurrentHero as HeroController;

            if (cell == null)
            {
                ReturnToOriginalPosition();
                return;
            }

            if (!cell.IsEmpty)
            {
                bool canMerge = targetUnit.Config.heroType == entity.Config.heroType
                                && targetUnit != entity
                                && targetUnit.CanMergeWith(entity);

                if (canMerge)
                {
                    if (targetUnit.TryMerge(entity))
                    {
                        entity = null;
                        Destroy(gameObject);
                        return;
                    }
                }
                else
                {
                    BoardSpawnCell oldCell = entity.CurrentCell;

                    if (oldCell != null && oldCell != cell)
                    {
                        oldCell.Clear();
                        cell.Clear();

                        entity.AssignCell(cell);
                        cell.AssignHero(entity);

                        targetUnit.AssignCell(oldCell);
                        oldCell.AssignHero(targetUnit);
                    }
                    else
                    {
                        ReturnToOriginalPosition();
                    }
                }
            }

            if (cell.IsEmpty)
            {
                cell.AssignHero(entity);
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        entity.CurrentCell?.AssignHero(entity);
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out var hit, 100f, LayerMask.GetMask("Ground", "Cell")))
        {
            return hit.point;
        }
        return transform.position;
    }
}