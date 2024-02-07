using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    Material defaultMat;

    Collider myCollider;
    [SerializeField]
    Collider targetCollider;

    public RocketMain mainRocketScript;

    public float snapDistance = 1f;
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped
    string snapBody = "BuildingBody";

    [SerializeField]
    public bool finishedPlacing = false;
    public bool isSnapping = false;

    Transform targetTransform;

    [SerializeField]
    Collider[] colliders;
    GameObject topSphere, bottomSphere;
    GameObject currentBottomSphere, currentTopSphere;

    Snapping bottomSnap, topSnap;
    RocketMain targetRocketScript;

    public RocketPart part;

    void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
        myCollider = GetComponent<Collider>();

        currentTopSphere = transform.GetChild(0).gameObject;
        currentBottomSphere = transform.GetChild(1).gameObject;

        topSnap = currentTopSphere.GetComponent<Snapping>();
        bottomSnap = currentBottomSphere.GetComponent<Snapping>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Spawning.spawnedObject = false;
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButtonDown(0) && !finishedPlacing)
        {
            finishedPlacing = true;
            Spawning.spawnedObject = false;
            GetComponent<Renderer>().material = defaultMat;

            this.tag = snapBody;

            switch (part)
            {
                case RocketPart.Body:
                    mainRocketScript = this.gameObject.AddComponent<Engine>();
                    break;
                case RocketPart.Fuel:
                    break;
                case RocketPart.Thruster:
                    break;
            }

            Destroy(this);
        }

        if (!finishedPlacing && !isSnapping)
        {
            Vector3 targetPos;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            targetPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Follow the mouse
            transform.position = targetPos;

            CheckSnap();
        }

    }

    void CheckSnap()
    {
        colliders = Physics.OverlapSphere(transform.position, snapDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(snapBody))
            {
                // Snap to the closest object
                targetTransform = collider.transform;
                targetCollider = targetTransform.GetComponent<Collider>();
                targetRocketScript = targetTransform.gameObject.GetComponent<RocketMain>();
                GetClosestPoint();
                break; // Snap to only the closest object
            }         
        }
    }

    private void GetClosestPoint()
    {
        topSphere = targetTransform.GetChild(0).gameObject;
        bottomSphere = targetTransform.GetChild(1).gameObject;

        Vector3 targetTopPosition = targetTransform.position + Vector3.up * (targetTransform.localScale.y * 2f);
        Vector3 targetBottomPosition = targetTransform.position - Vector3.up * (targetTransform.localScale.y * 2f);

        float checkTopSphere = Vector3.Distance(transform.position, targetTopPosition);
        float checkBottomSphere = Vector3.Distance(transform.position, targetBottomPosition);

        if (checkTopSphere > checkBottomSphere) //closer to bottom
        {
            bottomSphere.SetActive(true);
            topSphere.SetActive(false);

            currentTopSphere.SetActive(true);
            currentBottomSphere.SetActive(false);
        }
        else
        {
            bottomSphere.SetActive(false);
            topSphere.SetActive(true);

            currentBottomSphere.SetActive(true);
            currentTopSphere.SetActive(false);
        }
    }
}
