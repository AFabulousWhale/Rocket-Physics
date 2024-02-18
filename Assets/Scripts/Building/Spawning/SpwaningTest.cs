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
    BodySO fuel;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Spawning.SpawnObject(bodies.bodyList[0], bodies, bodies.part);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Spawning.SpawnObject(bodies.bodyList[1], bodies, bodies.part);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Spawning.SpawnObject(bodies.bodyList[2], bodies, bodies.part);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Spawning.SpawnObject(thrusters.thrusterList[0], thrusters, thrusters.part);
        }
    }
}
