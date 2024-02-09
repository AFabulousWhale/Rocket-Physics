using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMain : MonoBehaviour
{
    public bool canSnapToTop = true;
    public bool canSnapToBottom = true;
    public GameObject topSnappedObject, bottomSnappedObject;

    bool onMouse = false;

    Outline outline;

    private void OnMouseOver()
    {
        if (!onMouse)
        {
            if (outline == null)
            {
                outline = gameObject.AddComponent<Outline>();
                outline.OutlineColor = new Color32(0, 204, 0, 255);
                outline.OutlineWidth = 10f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                CheckSpheresToReset();

                transform.parent = null;
                Destroy(outline);
                gameObject.AddComponent<Visual>();

                onMouse = true;
            }
        }
    }

    void CheckSpheresToReset()
    {
        if(bottomSnappedObject != null) //if above another rocket part
        {
            ResetSnapping(bottomSnappedObject, 0); //resets the top sphere for the bottom part that it's deattached from
            ResetSnapping(gameObject, 1);
        }
    }

    void ResetSnapping(GameObject objectToReset, int child)
    {
        objectToReset.transform.GetChild(child).tag = "BuildingPart";
        objectToReset.transform.GetChild(child).gameObject.layer = 3;

        if (!objectToReset.transform.GetChild(child).gameObject.GetComponent<Snapping>())
        {
            objectToReset.transform.GetChild(child).gameObject.AddComponent<Snapping>();
        }
    }

    private void OnMouseExit()
    {
        Destroy(outline);
        if (onMouse)
        {
            onMouse = false;
        }    
    }
}
