using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwaningTest : MonoBehaviour
{
    [SerializeField]
    BodySO bodies;

    [SerializeField]
    ThrusterSO thrusters;

    [SerializeField]
    FuelSO fuel;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Spawning.SpawnBody(bodies.bodyList[0], bodies);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Spawning.SpawnBody(bodies.bodyList[1], bodies);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Spawning.SpawnBody(bodies.bodyList[2], bodies);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Spawning.SpawnThruster(thrusters.thrusterList[0], thrusters);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Spawning.SpawnFuelTank(fuel.fuelList[0], fuel);
        }
    }
}
