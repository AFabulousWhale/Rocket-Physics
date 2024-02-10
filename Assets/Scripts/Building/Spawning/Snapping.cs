using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped
    int layerMask = 1 << 3;
    Vector3 distanceToPivot;
    float snapDistance = 1.5f;

    Renderer rend;

    public Transform targetTransform;

    [SerializeField]
    Visual parentScript, targetParentScript;

    [SerializeField]
    public Snapping targetScript;

    GameObject parent;

    public bool canSnap = true; //used to determine if already snapped to something else

    [SerializeField]
    Collider[] colliders;
    private void Start()
    {
        parentScript = transform.parent.GetComponent<Visual>();
        rend = transform.parent.transform.GetComponent<Renderer>();
        parent = transform.parent.gameObject;
        canSnap = true;
    }

    private void Update()
    {
        if (parentScript)
        {
            if(parentScript.finishedPlacing)
            {
                if(targetScript != null && targetParentScript != null)
                {
                    if (targetParentScript.finishedPlacing)
                    {
                        canSnap = false;
                        targetScript.canSnap = false;
                        targetScript.targetScript = this;
                        targetScript.targetTransform = this.transform;

                        targetParentScript = null;
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
                        Debug.Log("No more snapping");
                        parentScript.isSnapping = false;
                    }
                }
            }
        }
    }

    void CheckSnap()
    {
        colliders = Physics.OverlapSphere(transform.position, snapDistance, layerMask);

        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag(snapTag))
            {
                if(hit.transform.parent.gameObject != parent && hit.gameObject.name != gameObject.name && hit.GetComponent<Snapping>().canSnap)
                { 
                    targetTransform = hit.transform;
                    targetScript = targetTransform.GetComponent<Snapping>();
                    targetParentScript = targetTransform.transform.parent.GetComponent<Visual>();
                    TrySnap(targetTransform);
                    break;
                }
                else
                {
                    targetTransform = null;
                    targetScript = null;
                    targetParentScript = null;
                    canSnap = true;
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

        Transform parentTransform = transform.parent;
        Vector3 parentPosition = parentTransform.position;
        Vector3 parentOffset = ((transform.position - parentPosition) + offset);

        parentPosition += parentOffset;

        Vector3 distance = transform.position - parentTransform.position;

        distance.y -= distance.y / Mathf.Abs(distance.y);

        //// Distance between part origin and sphere origin which is the bottom of the sphere so we add half the size to get to the center and then add the other half for the full offset

        parentPosition.y -= distance.y;

        parentTransform.position = parentPosition;

        Debug.Log($"{this.gameObject.name} snapped to {attachmentNode.gameObject.name}");
    }
}
