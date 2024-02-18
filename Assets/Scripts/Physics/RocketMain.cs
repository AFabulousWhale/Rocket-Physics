using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RocketMain : MonoBehaviour
{
    protected Rigidbody rb;
    public float mass;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        SetMass();
    }

    public virtual void SetMass()
    {
        rb.mass = mass * 100;
    }

    public float GetMass()
    {
        return mass;
    }
}
