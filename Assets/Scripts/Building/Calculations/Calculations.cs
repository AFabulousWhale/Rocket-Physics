using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculations : MonoBehaviour
{
    public static Calculations calcScriptRef;

    Calculations()
    {
        calcScriptRef = this;
    }
    private void Update()
    {
        //Debug.Log("Total Mass: " + GetTotalMass());
        //Debug.Log("Wind Resistance: " + GetWindResistance());
        //Debug.Log("Burn Time: " + GetBurnTime());
        //Debug.Log("Gravitational Force: " + GetGravitationalForce());
        //Debug.Log("Q: " + GetQ());
        //Debug.Log("X: " + GetX());

        //Debug.Log("Velocity: " + GetVelocity());
        //Debug.Log("Boost Phase: " + GetBoostPhaseDistance());
        //Debug.Log("Coast Phase: " + GetCoastPhaseDistance());
        
        Debug.Log("Altitude: " + GetTotalAltitude());
    }

    float GetGravitationalForce()
    {
        return GetTotalMass() * GetGravity();
    }

    float GetGravity()
    {
        return 9.8f;
    }

    public float GetThrust()
    {
        float thrust = 0;
        foreach (var item in RocketData.rocketData.rocketParts)
        {
            if (item.GetComponent<Thruster>())
            {
                thrust += item.GetComponent<Thruster>().GetThrust();
            }
        }
        return thrust;
    }

    public float GetBurnTime()
    {
        return GetFuel() / GetThrust();
    }

    float GetQ()
    {
        return Mathf.Sqrt((GetThrust() - (GetGravitationalForce())) / GetWindResistance());
    }

    float GetX()
    {
        return (2 * GetWindResistance() * GetQ()) / GetTotalMass();
    }

    public float GetVelocity()
    {
        return GetQ() * (1- Mathf.Exp(-GetX() * GetBurnTime())) / (1 + Mathf.Exp(-GetX() * GetBurnTime()));
    }

    public float GetFuel()
    {
        float fuel = 0;
        foreach (var item in RocketData.rocketData.rocketParts)
        {
            if (item.GetComponent<FuelFunctionality>())
            {
                fuel += item.GetComponent<FuelFunctionality>().currentFuelAmount;
            }
            
            if (item.GetComponent<Fuel>() && !item.GetComponent<FuelFunctionality>())
            {
                fuel += item.GetComponent<Fuel>().fuelAmount;
            }
        }
        return fuel;
    }

    float GetWindResistance()
    {
        return 0.5f * GetAirDensity() * GetDrag() * GetArea();
    }

    float GetArea()
    {
        return Mathf.PI * Mathf.Pow((0.5f * 0.976f / 12 * 0.3048f), 2);
    }

    float GetDrag()
    {
        return 0.75f; //set value for now
    }

    float GetAirDensity()
    {
        return 1.2f;
    }

    public float GetTotalAltitude()
    {
        if((GetBoostPhaseDistance() * 3.3f) + (GetCoastPhaseDistance() * 3.3f) <= 0)
        {
            return 0;
        }

        return (GetBoostPhaseDistance() * 3.3f) + (GetCoastPhaseDistance() * 3.3f);

    }

    float GetBoostPhaseDistance()
    {
        return (-GetTotalMass() / (2 * GetWindResistance())) * Mathf.Log((GetThrust() - (GetGravitationalForce()) - GetWindResistance() *Mathf.Pow(GetVelocity(), 2)) / (GetThrust() - (GetGravitationalForce())));
    }

    float GetCoastPhaseDistance()
    {
        return (GetTotalMass() / (2 * GetWindResistance())) * Mathf.Log((GetGravitationalForce() + GetWindResistance() * Mathf.Pow(GetVelocity(), 2)) / (GetGravitationalForce())); 
    }


    public float GetTotalMass()
    {
        float mass = 0.05398f;
        foreach (var item in RocketData.rocketData.rocketParts)
        {
            mass += item.GetComponent<RocketMain>().GetMass();
        }
        return mass;
    }
}
