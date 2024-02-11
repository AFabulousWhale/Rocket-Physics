using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped
    int layerMask = 1 << 3;
    float snapDistance = 1.5f;

    Renderer rend;

    public Transform detectedTransform, connectedTransform;

    [SerializeField]
    Visual parentScript, targetParentScript;

    [SerializeField]
    public Snapping targetScript;

    [SerializeField]
    public List<GameObject> childrenConnectedParts = new();

    [SerializeField]
    public GameObject parentConnectedPart;

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
        if(parent.transform.childCount > 2) //more children than the two spheres
        {
            for (int i = 2; i < parent.transform.childCount; i++)
            {
                if (!childrenConnectedParts.Contains(parent.transform.GetChild(i).gameObject)) //doesn't already contain the child
                {
                    childrenConnectedParts.Add(parent.transform.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            childrenConnectedParts.Clear();
        }

        if (childrenConnectedParts.Count > 0)
        {
            List<GameObject> tempList = new();
            foreach (var item in childrenConnectedParts)
            {
                if (item.transform.childCount > 2) //more children than the two spheres
                {
                    for (int i = 2; i < item.transform.childCount; i++)
                    {
                        if (!childrenConnectedParts.Contains(item.transform.GetChild(i).gameObject)) //doesn't already contain the child
                        {
                            tempList.Add(item.transform.GetChild(i).gameObject);
                        }
                    }
                }
            }

            foreach (var item in tempList)
            {
                childrenConnectedParts.Add(item);
            }
            tempList.Clear();
        }

        if(parent.transform.parent != null)
        {
            parentConnectedPart = parent.transform.parent.gameObject;
        }

        if (parentScript)
        {
            if(parentScript.hasPlaced)
            {
                if(targetScript != null && targetParentScript != null)
                {
                    targetScript.canSnap = false;
                    targetScript.targetScript = this;
                    targetScript.connectedTransform = this.transform;

                    connectedTransform = detectedTransform;
                    targetScript.detectedTransform = null;
                    detectedTransform = null;

                    if(parentScript.targetTransform == null)
                    {
                        parentScript.targetTransform = connectedTransform.parent;

                        transform.parent.parent = parentScript.targetTransform;
                    }

                    targetParentScript = null;
                }
            }
            else //checks radius whilst sphere isn't placed (following mouse)
            {
                CheckSnap(gameObject);

                if (detectedTransform)
                {
                    Vector3 targetPos;
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10f;
                    targetPos = Camera.main.ScreenToWorldPoint(mousePos);
                    if (Vector3.Distance(targetPos, detectedTransform.position) > (snapDistance * 2)) //mouse out of radius of sphere
                    {
                        Debug.Log("No more snapping");
                        canSnap = true;
                        parentScript.inRadiusOfSphere = false;
                    }
                }
            }
        }
    }

    void CheckSnap(GameObject objectToCheckSnap)
    {
        colliders = Physics.OverlapSphere(transform.position, snapDistance, layerMask);

        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag(snapTag))
            {
                if(hit.transform.parent.gameObject != parent && hit.gameObject.name != gameObject.name && hit.GetComponent<Snapping>().canSnap && !childrenConnectedParts.Contains(hit.transform.parent.gameObject) && hit.transform.parent != parentConnectedPart)
                { 
                    detectedTransform = hit.transform;
                    targetScript = detectedTransform.GetComponent<Snapping>();
                    targetParentScript = detectedTransform.transform.parent.GetComponent<Visual>();
                    TrySnap(detectedTransform, transform, 0);
                    break;
                }
                else //can't find target anymore
                {
                    if (hit.gameObject != connectedTransform) //unless it's already connected
                    {
                        detectedTransform = null;
                        targetScript = null;
                        targetParentScript = null;
                    }
                }
            }
        }
    }

    void TrySnap(Transform attachmentNode, Transform partToAttach, float yOffset)
    {
        parentScript.inRadiusOfSphere = true;
        // Calculate the offset and rotation for snapping
        Vector3 offset = attachmentNode.position - partToAttach.transform.position;
        Quaternion rotation = Quaternion.FromToRotation(partToAttach.transform.up, attachmentNode.up);

        Transform parentTransform = partToAttach.transform.parent;
        Vector3 parentPosition = parentTransform.position;
        Vector3 parentOffset = ((partToAttach.transform.position - parentPosition) + offset);

        parentPosition += parentOffset;

        Vector3 distance = partToAttach.transform.position - parentTransform.position;

        distance.y -= distance.y / Mathf.Abs(distance.y);

        //// Distance between part origin and sphere origin which is the bottom of the sphere so we add half the size to get to the center and then add the other half for the full offset

        parentPosition.y -= distance.y;

        parentPosition.y -= yOffset;

        parentTransform.position = parentPosition;

        Debug.Log($"{this.gameObject.name} snapped to {attachmentNode.gameObject.name}");
        canSnap = false;
    }
}
