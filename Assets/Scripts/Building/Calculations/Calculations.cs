using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculations : MonoBehaviour
{
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

    float GetThrust()
    {
        float thrust = 0;
        foreach (var item in RocketData.rocketData.rocketPartsOrder)
        {
            if (item.GetComponent<Thruster>())
            {
                thrust += item.GetComponent<Thruster>().GetThrust();
            }
        }
        return thrust;
    }

    float GetBurnTime()
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

    float GetVelocity()
    {
        return GetQ() * (1- Mathf.Exp(-GetX() * GetBurnTime())) / (1 + Mathf.Exp(-GetX() * GetBurnTime()));
    }

    float GetFuel()
    {
        float fuel = 0;
        foreach (var item in RocketData.rocketData.rocketPartsOrder)
        {
            if (item.GetComponent<Fuel>())
            {
                fuel += item.GetComponent<Fuel>().GetFuel();
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

    float GetTotalAltitude()
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


    float GetTotalMass()
    {
        float mass = 0.05398f;
        foreach (var item in RocketData.rocketData.rocketPartsOrder)
        {
            mass += item.GetComponent<RocketMain>().GetMass();
        }
        return mass;
    }
}
