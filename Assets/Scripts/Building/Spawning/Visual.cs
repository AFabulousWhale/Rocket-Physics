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
            if (targetTransform && isSnapping)
            {
                targetTransform.tag = "Untagged";
            }
            Destroy(this);
            //remove this script
            //add part script
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
                SnapTo();
                break; // Snap to only the closest object
            }
            else
            {
                isSnapping = false;
            }
        }
    }

    void SnapTo()
    {
        isSnapping = true;

        Vector3 targetTopPosition = targetTransform.position + Vector3.up * (targetTransform.localScale.y / 2f); //gets the top point of the target sphere

        transform.position = targetTopPosition;

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
