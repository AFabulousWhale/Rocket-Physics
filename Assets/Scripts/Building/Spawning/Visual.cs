using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    Material defaultMat;

    Collider myCollider;
    [SerializeField]
    Collider targetCollider;
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
            Destroy(this);
            //remove this script
            //add part script
        }

        CheckSnap();
    }
    public float snapDistance = 0.5f;
    public LayerMask snapLayerMask; // Set this to the layer where the objects can be snapped
    public string snapTag = "BuildingPart"; // Set this to the tag of objects that can be snapped

    private bool isSnapping = false;

    void CheckSnap()
    {
        if (!isSnapping)
        {
            // Follow the mouse
            Vector3 targetPos;
            Vector2 mousePos = Input.mousePosition;
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            targetPos.z = transform.position.z;
            transform.position = targetPos;

            // Check for objects to snap to
            Collider[] colliders = Physics.OverlapSphere(transform.position, snapDistance, snapLayerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(snapTag))
                {
                    // Snap to the closest object
                    targetCollider = collider.transform.GetComponent<Collider>();
                    SnapTo();
                    break; // Snap to only the closest object
                }
            }
        }

        if(isSnapping)
        {
            Vector3 targetPos;
            Vector2 mousePos = Input.mousePosition;
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            if (targetPos.magnitude > (snapDistance * 2))
            {
                isSnapping = false;
            }
        }
    }

    void SnapTo()
    {
        isSnapping = true;

        Vector3 myClosestPos = myCollider.ClosestPoint(targetCollider.transform.position);
        Vector3 targetClosestPos = targetCollider.ClosestPoint(myClosestPos);

        Vector3 offset = targetClosestPos - myClosestPos;

        if (offset.magnitude < snapDistance)
        {
            transform.position += offset;

            Quaternion relativeRotation = Quaternion.FromToRotation(transform.up, targetCollider.transform.up);
            transform.rotation = relativeRotation * transform.rotation;
        }
    }
}
