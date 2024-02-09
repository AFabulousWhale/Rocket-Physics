using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapping
{
    public void Snapping(GameObject objectToSnapTo, GameObject objectSnapping);
    public void Deataching(GameObject objectToDetachFrom);
}
