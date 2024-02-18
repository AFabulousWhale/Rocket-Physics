using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rocket Parts/Fuel Tanks")]
public class FuelSO : RocketMainSO
{
    public List<FuelData> fuelList;
}

[Serializable]
public class FuelData : MainRocketData
{
    [SerializeField]
    public float fuelAmount;

    [SerializeField]
    public float wetMass;
}
