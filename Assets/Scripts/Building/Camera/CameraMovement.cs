using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 origin = new(0, 2.3f, 0);

    Vector3 previousPos;

    Camera mainCamera;

    float zoomScale = 10;

    [SerializeField]
    Vector3 diff, dragOrigin;
    [SerializeField]
    bool drag;
    Vector3 startOrigin;
    Quaternion startRot;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        startOrigin = transform.position;
        startRot = transform.rotation;
    }

    void LateUpdate()
    {
        //pressing middle mouse = move screen
        //pressing right mouse = rotate screen
        //scrolling = zoom in/out

        Rotating();
        Zooming();
        //Dragging();
    }

    /// <summary>
    /// checks scroll input to drag the screen and move the "origin point"
    /// </summary>
    private void Dragging()
    {
        if(Input.GetMouseButton(0))
        {
            diff = GetMouseWorld - transform.position;
            if (!drag)
            {
                dragOrigin = GetMouseWorld;
                drag = true;
            }
        }
        else
        {
            drag = false;
        }

        if(drag)
        {
            transform.position = dragOrigin - diff;
        }

        if(Input.GetKey(KeyCode.F))
        {
            transform.position = startOrigin;
            transform.rotation = startRot;
            mainCamera.fieldOfView = 60;
            origin = new(0, 2.3f, 0);
        }
    }

    /// <summary>
    /// checks scroll wheel input to "zoom in and out"
    /// </summary>
    private void Zooming()
    {
        mainCamera.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * -zoomScale;
    }

    /// <summary>
    /// checks mouse input to rotate camera around centre object
    /// </summary>
    void Rotating()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPos = GetMouseViewport;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPos - GetMouseViewport;

            transform.position = origin;

            transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            transform.Translate(new Vector3(0, 0, -10));

            previousPos = GetMouseViewport;
        }
    }

    Vector3 GetMouseViewport => mainCamera.ScreenToViewportPoint(Input.mousePosition);
    Vector3 GetMouseWorld => mainCamera.ScreenToWorldPoint(Input.mousePosition);
}
