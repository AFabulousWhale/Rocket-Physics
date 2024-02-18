using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelFunctionality : MonoBehaviour
{
    public float currentFuelAmount;
    public float startingFuel;
    public float massPerL;

    public float depletionRate;

    public float wetMass;

    [SerializeField]
    float dryMass;

    float startWetMass;

    Rigidbody rb;

    private void Start()
    {
        wetMass = currentFuelAmount * massPerL;
        rb = GetComponent<Rigidbody>();
        rb.mass += (wetMass + dryMass);

        startingFuel = currentFuelAmount;
        startWetMass = wetMass;
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            UseFuel();
        }
    }

    void UseFuel()
    {
        currentFuelAmount -= depletionRate / 10;
        currentFuelAmount = Mathf.Clamp(currentFuelAmount, 0, startingFuel);
        wetMass = startWetMass * (currentFuelAmount / 100);
        rb.mass = (wetMass + dryMass);
    }
}
