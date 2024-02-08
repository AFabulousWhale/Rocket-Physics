using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMain : MonoBehaviour
{
    public bool canSnapToTop = true;
    public bool canSnapToBottom = true;
    public GameObject topObject, bottomObject;

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
                transform.parent = null;
                Destroy(outline);
                gameObject.AddComponent<Visual>();



                onMouse = true;
                Destroy(gameObject.GetComponent<RocketMain>());
            }
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
