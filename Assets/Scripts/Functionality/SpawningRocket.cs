using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningRocket : MonoBehaviour
{
    [SerializeField]
    GameObject rocketPrefab;

    GameObject spawnedRocket;

    [SerializeField]
    GameObject fireParticle;

    // Start is called before the first frame update
    void Start()
    {
        spawnedRocket = Instantiate(rocketPrefab, new Vector3(0, 1.4f, 0), Quaternion.identity);

        DestroyScripts(spawnedRocket.transform);

        Camera.main.transform.parent = spawnedRocket.transform.GetChild(0);

        foreach (var item in RocketData.rocketData.rocketParts)
        {
            RocketScriptsCheck(item.transform);
        }
    }

    void DestroyScripts(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Snapping>())
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            if (transform.GetChild(i).GetComponent<Visual>())
            {
                RocketData.rocketData.rocketParts.Add(transform.GetChild(i).gameObject);
                transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                Destroy(transform.GetChild(i).GetComponent<Visual>());
                DestroyScripts(transform.GetChild(i));
            }
        }
    }

    void RocketScriptsCheck(Transform transform)
    {
        if(transform.GetComponent<Thruster>())
        {
            Thruster thrusterRef = transform.GetComponent<Thruster>();
            ThrusterFunctionality thrusterScript = transform.gameObject.AddComponent<ThrusterFunctionality>();

            thrusterScript.thrustAmount = thrusterRef.thrustAmount;;
            GameObject spawnedfire = Instantiate(fireParticle, new Vector3(transform.position.x, transform.GetComponent<Collider>().bounds.min.y, transform.position.z), Quaternion.identity);
            thrusterScript.fireParticle = spawnedfire;
            spawnedfire.transform.parent = transform;
        }

        if (transform.GetComponent<Fuel>())
        {
            Fuel fuelRef = transform.GetComponent<Fuel>();
            FuelFunctionality fuelScript = transform.gameObject.AddComponent<FuelFunctionality>();

            fuelScript.wetMass = fuelRef.wetMass;
            fuelScript.dryMass = fuelRef.mass;
            fuelScript.currentFuelAmount = fuelRef.fuelAmount;
            fuelScript.depletionRate = Calculations.calcScriptRef.GetBurnTime();
        }
    }
}
