using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainRocketData
{
    [SerializeField]
    public string bodyName;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public float dryMass;
}
