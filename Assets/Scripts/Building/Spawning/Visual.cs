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

    public float snapDistance = 1f;
    public LayerMask snapLayerMask = 1; // Set this to the layer where the objects can be snapped
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped

    [SerializeField]
    private bool isSnapping = false;

    Transform targetTransform;

    [SerializeField]
    Collider[] colliders;
    GameObject topSphere, bottomSphere;
    RocketMain snappedScript;
    bool isSnapTop;

    public RocketPart part;

    void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
        myCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Spawning.spawnedObject = false;
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Spawning.spawnedObject = false;
            GetComponent<Renderer>().material = defaultMat;
            this.tag = snapTag;

            RocketMain thisSnap = new();

            switch (part)
            {
                case RocketPart.Body:
                    thisSnap = this.gameObject.AddComponent<Engine>();
                    break;
                case RocketPart.Fuel:
                    break;
                case RocketPart.Thruster:
                    break;
            }

            if (targetTransform && isSnapping)
            {
                if (isSnapTop)
                {
                    snappedScript.topSnap = false;
                    snappedScript.topObject = this.gameObject;
                    thisSnap.bottomObject = targetTransform.gameObject;
                    thisSnap.bottomSnap = false;
                }
                else
                {
                    snappedScript.bottomSnap = false;
                    snappedScript.bottomObject = this.gameObject;
                    thisSnap.topObject = targetTransform.gameObject;
                    thisSnap.topSnap = false;
                }

                if(snappedScript.topObject && snappedScript.bottomObject)
                {
                    targetTransform.tag = "Untagged";
                }
            }
            Destroy(this);
        }
        else
        {
            CheckSnap();
        }
    }

    void CheckSnap()
    {
        Vector3 targetPos;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Follow the mouse
        transform.position = targetPos;

        // Check for objects to snap to
        colliders = Physics.OverlapSphere(transform.position, snapDistance, snapLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(snapTag))
            {
                // Snap to the closest object
                targetTransform = collider.transform;
                targetCollider = targetTransform.GetComponent<Collider>();
                snappedScript = targetTransform.gameObject.GetComponent<RocketMain>();
                GetClosestPoint();
                break; // Snap to only the closest object
            }
            else
            {
                isSnapping = false;
                if (topSphere && bottomSphere)
                {
                    topSphere.SetActive(false);
                    bottomSphere.SetActive(false);
                }
                if (snappedScript)
                {
                    if (!snappedScript.bottomObject)
                    {
                        snappedScript.bottomSnap = true;
                    }
                    if (!snappedScript.topObject)
                    {
                        snappedScript.topSnap = true;
                    }
                }
            }
        }
    }

    private void GetClosestPoint()
    {
        Vector3 targetTopPosition = targetTransform.position + Vector3.up * (targetTransform.localScale.y * 2f);
        Vector3 targetBottomPosition = targetTransform.position - Vector3.up * (targetTransform.localScale.y * 2f);

        topSphere = targetTransform.GetChild(0).gameObject;
        bottomSphere = targetTransform.GetChild(1).gameObject;

        float checkTopSphere = Vector3.Distance(transform.position, targetTopPosition);
        float checkBottomSphere = Vector3.Distance(transform.position, targetBottomPosition);

        if(checkTopSphere > checkBottomSphere) //closer to bottom
        {
            if (snappedScript.bottomSnap)
            {
                bottomSphere.SetActive(true);
                topSphere.SetActive(false);
                isSnapTop = false;
                if (!snappedScript.topObject)
                {
                    snappedScript.topSnap = true;
                }
                SnapTo(targetBottomPosition);
            }
        }
        else
        {
            if (snappedScript.topSnap)
            {
                topSphere.SetActive(true);
                bottomSphere.SetActive(false);
                isSnapTop = true;
                if(!snappedScript.bottomObject)
                {
                    snappedScript.bottomSnap = true;
                }
                SnapTo(targetTopPosition);
            }
        }
    }

    void SnapTo(Vector3 posToSnapTo)
    {
        //bottom links to bottom of top sphere
        //top links to top of bottom sphere

        //gets closest point of target
        //if nearest top point show top sphere
        //if nearest bottom point show bottom sphere

        //if slightly closer then snap to respective sphere

        isSnapping = true;

        transform.position = posToSnapTo;

        Quaternion relativeRotation = Quaternion.FromToRotation(transform.up, targetCollider.transform.up);
        transform.rotation = relativeRotation * transform.rotation;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, snapDistance);
    }
}
