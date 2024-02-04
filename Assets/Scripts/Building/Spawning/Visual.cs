using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    Material defaultMat;
    void Awake()
    {

        defaultMat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Spawning.spawnedObject = false;
            Destroy(this.gameObject);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Spawning.spawnedObject = false;
            GetComponent<Renderer>().material = defaultMat;
            Destroy(this);
            //remove this script
            //add part script
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos;
        Vector2 mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }
}
