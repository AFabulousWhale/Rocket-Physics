using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    Rigidbody rb;
    public float thrustAmount = 20f;

    GameObject fireParticle;

    public float verticalInput;
    public float horizontalInput;
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

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        verticalInput = input.y * 8;
        verticalInput = Mathf.Clamp(verticalInput, -8, 8);

        horizontalInput = input.x * -8;
        horizontalInput = Mathf.Clamp(horizontalInput, -8, 8);


        transform.eulerAngles = new Vector3(verticalInput, 0, horizontalInput);
    }
}
