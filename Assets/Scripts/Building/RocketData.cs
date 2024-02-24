using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketData : MonoBehaviour
{
    public static RocketData rocketData;

    public List<GameObject> rocketParts = new();
    public GameObject rocketParent;
    public GameObject rocketPartParent;

    RocketData ()
    {
        rocketData = this;
    }
}
