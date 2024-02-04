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
            Spawning.SpawnObject(objects, 1);
        }
    }
}
