using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Thruster : MonoBehaviour
{
    Rigidbody rb;
    public float thrustAmount;

    GameObject fireParticle;
    public TextMeshProUGUI heightText, velocityText;

    public float verticalInput;
    public float horizontalInput;

    public Fuel fuel;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fireParticle = transform.GetChild(0).gameObject;

        thrustAmount = ((fuel.depletionRate * fuel.massPerL) * 4) * 100;
    }

    void FixedUpdate()
    {
        if(rb.velocity.y > 20)
        {
            fuel.depletionRate = 1;
        }

        if (Input.GetButton("Jump"))
        {
            IsFlying();
        }
        else
        {
            fireParticle.SetActive(false);
        }

        heightText.text = "Height:" + transform.position.y.ToString();
        velocityText.text = "Velocity:" + rb.velocity.y.ToString();
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
