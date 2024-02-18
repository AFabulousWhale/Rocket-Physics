using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : RocketMain
{
    public float wetMass;
    public float fuelAmount;

    public override void SetMass()
    {
        mass += wetMass;
        rb.mass = mass * 100;
    }

    public float GetFuel()
    {
        return fuelAmount;
    }
}
