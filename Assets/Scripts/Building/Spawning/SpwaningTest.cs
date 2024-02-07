using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwaningTest : MonoBehaviour
{
    [SerializeField]
    BodySO objects;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Spawning.SpawnObject(objects.bodyList[0], objects, objects.part);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Spawning.SpawnObject(objects.bodyList[1], objects, objects.part);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Spawning.SpawnObject(objects.bodyList[2], objects, objects.part);
        }
    }
}
