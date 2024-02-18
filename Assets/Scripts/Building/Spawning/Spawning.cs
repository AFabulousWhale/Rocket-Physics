using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Spawning
{
    public static bool spawnedObject = false;

    static RocketMain rocketScript;
    public static void SpawnObject(MainRocketData partToSpawn, RocketMainSO rocketMain, RocketPart part)
    {
        if (!spawnedObject)
        {
            GameObject newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(partToSpawn.prefab);

            Visual visual = newPrefab.AddComponent<Visual>();
            newPrefab.GetComponent<Renderer>().material = rocketMain.visualMat;

            switch (part)
            {
                case RocketPart.Body:
                    rocketScript = newPrefab.AddComponent<Engine>();
                    break;
                case RocketPart.Fuel:
                    rocketScript = newPrefab.AddComponent<Fuel>();
                    break;
                case RocketPart.Thruster:
                    rocketScript = newPrefab.AddComponent<Thruster>();
                    newPrefab.GetComponent<Thruster>().thrustAmount = 5;
                    break;
            }

            rocketScript.mass = partToSpawn.dryMass;

            spawnedObject = true;
        }
    }
}
