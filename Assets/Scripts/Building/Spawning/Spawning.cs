using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Spawning
{
    public static bool spawnedObject = false;

    public static void SpawnBody(BodyData partToSpawn, BodySO rocketMain)
    {
        GameObject newPrefab = Spawn(partToSpawn, rocketMain);
        Engine rocketScript = newPrefab.AddComponent<Engine>();
        rocketScript.mass = partToSpawn.dryMass;
    }

    public static void SpawnFuelTank(FuelData partToSpawn, FuelSO rocketMain)
    {
        GameObject newPrefab = Spawn(partToSpawn, rocketMain);
        Fuel rocketScript = newPrefab.AddComponent<Fuel>();
        rocketScript.mass = partToSpawn.dryMass;
        rocketScript.fuelAmount = partToSpawn.fuelAmount;
        rocketScript.wetMass = partToSpawn.wetMass;
    }

    public static void SpawnThruster(ThrusterData partToSpawn, ThrusterSO rocketMain)
    {
        GameObject newPrefab = Spawn(partToSpawn, rocketMain);
        Thruster rocketScript = newPrefab.AddComponent<Thruster>();
        rocketScript.mass = partToSpawn.dryMass;
        rocketScript.thrustAmount = partToSpawn.thrustAmount;
    }

    public static GameObject Spawn(MainRocketData partToSpawn, RocketMainSO rocketMain)
    {
        GameObject newPrefab = null;
        if (!spawnedObject)
        {
            newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(partToSpawn.prefab);

            Visual visual = newPrefab.AddComponent<Visual>();
            //newPrefab.GetComponent<Renderer>().material = rocketMain.visualMat;
            spawnedObject = true;
        }
        return newPrefab;
    }
}
