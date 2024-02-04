using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 origin = new(0, 2.3f, 0);

    Vector3 previousPos;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        //pressing middle mouse = move screen
        //pressing right mouse = rotate screen
        //scrolling = zoom in/out

        if(Input.GetMouseButtonDown(1))
        {
            previousPos = GetMousePosition;
        }

        if(Input.GetMouseButton(1))
        {
            Vector3 direction = previousPos - GetMousePosition;

            transform.position = origin;

            transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            transform.Translate(new Vector3(0, 0, -10));

            previousPos = GetMousePosition;
        }
    }

    Vector3 GetMousePosition => mainCamera.ScreenToViewportPoint(Input.mousePosition);
}
