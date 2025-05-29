using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Camera cam;
    private Entity selectedUnit;
    private Vector3 offset;
    private Vector3 oldPosition;
    private float fixedY = 0.5f;
    private Plane dragPlane;

    void Start()
    {
        cam = Camera.main;
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));
        oldPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                selectedUnit = hit.collider.GetComponent<Entity>();
                if (selectedUnit != null)
                {
                    // Подсчёт смещения между позицией юнита и курсором
                    offset = selectedUnit.transform.position - hit.point;
                }
            }
        }

        if (Input.GetMouseButton(0) && selectedUnit != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                selectedUnit.transform.position = new Vector3(
                    hitPoint.x + offset.x,
                    fixedY,
                    hitPoint.z + offset.z
                );
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedUnit != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var target = hit.collider.GetComponent<Entity>();

                if (target != null && target != selectedUnit)
                {
                    selectedUnit.Merge(target);
                }
            }

            // Можно вернуть на фиксированную позицию или оставить на месте
            selectedUnit.transform.position = new Vector3(
                oldPosition.x,
                oldPosition.y,
                oldPosition.z
            );

            selectedUnit = null;
        }
    }
}
