using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Spawning
{
    public static bool spawnedObject = false;
    public static void SpawnObject(MainRocketData partToSpawn, RocketMainSO rocketMain, RocketPart part)
    {
        if (!spawnedObject)
        {
            GameObject newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(partToSpawn.prefab);

            Visual visual = newPrefab.AddComponent<Visual>();
            newPrefab.GetComponent<Renderer>().material = rocketMain.visualMat;
            visual.part = part;

            spawnedObject = true;
        }
    }
}
