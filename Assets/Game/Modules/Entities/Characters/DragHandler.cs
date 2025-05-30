using UnityEngine;

public class DragHandler : MonoBehaviour
{
    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private LayerMask cellLayer;
    [SerializeField] private float fixedY = 0.5f;

    private Camera cam;
    private Entity selectedUnit;
    private Vector3 originalPosition;
    private Plane dragPlane;
    private bool isDragging;

    private void Awake()
    {
        cam = Camera.main;
        dragPlane = new Plane(Vector3.up, Vector3.up * fixedY);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleInput(touch.phase, touch.position);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                HandleInput(TouchPhase.Began, Input.mousePosition);
            else if (Input.GetMouseButton(0))
                HandleInput(TouchPhase.Moved, Input.mousePosition);
            else if (Input.GetMouseButtonUp(0))
                HandleInput(TouchPhase.Ended, Input.mousePosition);
        }
    }

    private void HandleInput(TouchPhase phase, Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);

        switch (phase)
        {
            case TouchPhase.Began:
                if (Physics.Raycast(ray, out RaycastHit hit, 10000f, unitLayer))
                {
                    selectedUnit = hit.collider.GetComponent<Entity>();
                    if (selectedUnit != null)
                    {
                        originalPosition = selectedUnit.transform.position;
                        isDragging = true;
                    }
                }
                break;

            case TouchPhase.Moved:
                if (isDragging && selectedUnit != null)
                {
                    if (dragPlane.Raycast(ray, out float distance))
                    {
                        Vector3 worldPoint = ray.GetPoint(distance);
                        selectedUnit.transform.position = new Vector3(worldPoint.x, fixedY, worldPoint.z);
                    }
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (isDragging && selectedUnit != null)
                {
                    Ray rayDown = new Ray(selectedUnit.transform.position, Vector3.down);
                    if (Physics.Raycast(rayDown, out RaycastHit dropHit, 10000f, cellLayer))
                    {
                        BoardSpawnCell cell = dropHit.collider.GetComponent<BoardSpawnCell>();

                        if (cell != null)
                        {
                            Entity targetUnit = cell.CurrentHero;
                            if (targetUnit != null && targetUnit.Config.heroType == selectedUnit.Config.heroType && targetUnit != selectedUnit)
                            {
                                bool merged = targetUnit.Merge(selectedUnit);
                                if (!merged)
                                {
                                    selectedUnit.transform.position = originalPosition;
                                }
                            }
                            else
                            {
                                //if (cell.IsEmpty)
                                //    selectedUnit.ChangeCell(cell);
                                //else
                                    selectedUnit.transform.position = originalPosition;
                            }
                        }
                        else
                        {
                            selectedUnit.transform.position = originalPosition;
                        }
                    }
                    else
                    {
                        selectedUnit.transform.position = originalPosition;
                    }

                    selectedUnit = null;
                    isDragging = false;
                }
                break;
        }
    }
}