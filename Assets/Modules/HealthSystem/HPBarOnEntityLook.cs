using UnityEngine;

public class HPBarOnEntityLook : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
