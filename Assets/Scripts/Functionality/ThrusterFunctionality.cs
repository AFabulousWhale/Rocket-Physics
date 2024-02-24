using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrusterFunctionality : MonoBehaviour
{
    Rigidbody rb;
    public float thrustAmount;

    GameObject fireParticle;

    public float verticalInput;
    public float horizontalInput;

    public FuelFunctionality fuel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fireParticle = transform.GetChild(0).gameObject;

        thrustAmount = ((fuel.depletionRate * fuel.massPerL) * 4) * 100;

        thrustAmount = 1000;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 20)
        {
            fuel.depletionRate = 1;
        }

        if (Input.GetButton("Jump"))
        {
            if (fuel.currentFuelAmount > 0)
            {
                IsFlying();
            }
            else
            {
                fireParticle.SetActive(false);
            }
        }
        else
        {
            fireParticle.SetActive(false);
        }
    }

    void IsFlying()
    {
        //Apply a force to this Rigidbody in direction of this GameObjects up axis
        fireParticle.SetActive(true);
        rb.AddForce(transform.up * thrustAmount);
        Debug.DrawLine(transform.position, transform.up, Color.green, thrustAmount);

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        verticalInput = input.y * 8;
        verticalInput = Mathf.Clamp(verticalInput, -8, 8);

        horizontalInput = input.x * -8;
        horizontalInput = Mathf.Clamp(horizontalInput, -8, 8);


        transform.eulerAngles = new Vector3(verticalInput, 0, horizontalInput);
    }
}
