using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMainSO : ScriptableObject
{
    public Material visualMat;
    public RocketPart part;
}

public enum RocketPart
{
    Body,
    Fuel,
    Thruster
}
