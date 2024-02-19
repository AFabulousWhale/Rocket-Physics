using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainRocketData
{
    [SerializeField]
    public string bodyName;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public float dryMass;
    [SerializeField]
    public Sprite icon;
}
