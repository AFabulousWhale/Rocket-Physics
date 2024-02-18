using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rocket Parts/Thrusters")]
public class ThrusterSO : RocketMainSO
{
    public List<ThrusterData> thrusterList;
}

[Serializable]
public class ThrusterData : MainRocketData
{
}
