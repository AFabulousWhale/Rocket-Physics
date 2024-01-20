using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    Rigidbody rb;
    public float thrustAmount = 20f;
    GameObject fireParticle;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fireParticle = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            fireParticle.SetActive(true);
            rb.AddForce(transform.up * thrustAmount);
        }
        else
        {
            fireParticle.SetActive(false);
        }
    }
}
