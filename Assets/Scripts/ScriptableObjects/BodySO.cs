using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rocket Parts/Bodies")]
public class BodySO : ScriptableObject
{
    public Material visualMat;
    public List<BodyData> bodyList;
}

[Serializable]
public class BodyData
{
    [SerializeField]
    public string bodyName;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public int dryMass;
}
