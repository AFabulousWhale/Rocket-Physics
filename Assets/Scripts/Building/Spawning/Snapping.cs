using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped
    string topSnapName = "TopSphere";
    string bottomSnapName = "BottomSphere";
    int snapLayerInt = 1 << 3;
    Vector3 distanceToPivot;
    float snapDistance = 1.5f;

    public Transform targetTransform;
    [SerializeField]
    private RocketMain targetRocketScript, thisRocketScript;

    Visual parentScript;

    [SerializeField]
    Collider[] colliders;


    private void Start()
    {
        parentScript = transform.parent.GetComponent<Visual>();
    }

    private void Update()
    {
        if (parentScript)
        {
            if (parentScript.finishedPlacing)
            {
                thisRocketScript = parentScript.mainRocketScript;


                if (targetTransform)
                {
                    if (targetTransform.gameObject.name == bottomSnapName)
                    {
                        targetRocketScript.canSnapToBottom = false;
                        targetRocketScript.bottomObject = this.gameObject;
                        targetTransform.tag = "Untagged";
                        targetTransform.gameObject.layer = 0;

                        this.tag = "Untagged";
                        this.gameObject.layer = 0;
                        thisRocketScript.topObject = targetTransform.gameObject;
                        thisRocketScript.canSnapToTop = false;

                        this.gameObject.SetActive(false);
                        Destroy(this);
                    }
                    else if (targetTransform.gameObject.name == topSnapName)
                    {
                        targetRocketScript.canSnapToTop = false;
                        targetRocketScript.topObject = this.gameObject;
                        targetTransform.tag = "Untagged";
                        targetTransform.gameObject.layer = 0;

                        this.tag = "Untagged";
                        this.gameObject.layer = 0;
                        thisRocketScript.bottomObject = targetTransform.gameObject;
                        thisRocketScript.canSnapToBottom = false;

                        this.gameObject.SetActive(false);
                        Destroy(this);
                    }
                    else
                    {
                        Debug.LogError("The name of the snap is incorrect");
                    }
                }
            }
            else
            {
                CheckSnap();

                if (targetTransform)
                {
                    Vector3 targetPos;
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10f;
                    targetPos = Camera.main.ScreenToWorldPoint(mousePos);
                    if (Vector3.Distance(targetPos, targetTransform.position) > (snapDistance * 2))
                    {
                        parentScript.isSnapping = false;
                    }
                }
            }
        }
    }

    void CheckSnap()
    {
        distanceToPivot = CalculatePivotToCenterDistance();

        // Output the distance
        Debug.Log($"Distance from pivot to center for {transform.gameObject.name}: {distanceToPivot}");

        colliders = Physics.OverlapSphere(transform.position, snapDistance, snapLayerInt);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(snapTag))
            {
                if (collider.transform.parent != transform.parent) //wont snap to self
                {
                    targetTransform = collider.transform;
                    targetRocketScript = targetTransform.transform.parent.GetComponent<RocketMain>();
                    // Snap to the closest object
                    TrySnap(targetTransform);
                    break; // Snap to only the closest object
                }
            }
        }
    }
    void TrySnap(Transform attachmentNode)
    {
        parentScript.isSnapping = true;
        // Calculate the offset and rotation for snapping
        Vector3 offset = attachmentNode.position - transform.position;
        Quaternion rotation = Quaternion.FromToRotation(transform.up, attachmentNode.up);

        Transform parent = transform.parent;
        Vector3 parentPosition = parent.transform.position;
        Vector3 parentOffset = ((transform.position - parentPosition) + offset);
        parentPosition += parentOffset;
        parentPosition += distanceToPivot;
        parent.transform.position = parentPosition;

    }

    Vector3 CalculatePivotToCenterDistance()
    {
        // Get the mesh filter of the object
        Renderer rend = transform.parent.transform.GetComponent<Renderer>();

        //// Calculate the distance from the pivot to the center of the mesh
        //float distance = Vector3.Distance(transform.parent.transform.position, rend.bounds.center);

        Vector3 distance = transform.parent.transform.position - rend.bounds.center;

        return distance;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, snapDistance);
    }
}
