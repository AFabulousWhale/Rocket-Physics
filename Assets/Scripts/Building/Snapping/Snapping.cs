using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    Vector3 targetPos;
    Collider myCollider;
    [SerializeField]
    Collider targetCollider;
    public float snapDistance = 3;
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void Update()
    {
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
