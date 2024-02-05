using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rocket Parts/Bodies")]
public class BodySO : RocketMainSO
{
    public List<BodyData> bodyList;
}

[Serializable]
public class BodyData : MainRocketData
{
    //more specific stuff to do with body
    [SerializeField]
    public string test;
}
