using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Spawning
{
    public static bool spawnedObject = false;
    public static void SpawnObject(BodySO partToSpawn, int index)
    {
        if (!spawnedObject)
        {
            GameObject newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(partToSpawn.bodyList[index].prefab);

            Visual visual = newPrefab.AddComponent<Visual>();
            newPrefab.GetComponent<Renderer>().material = partToSpawn.visualMat;
            spawnedObject = true;
        }
    }
}
