using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    Material defaultMat;

    public RocketMain mainRocketScript;

    [SerializeField]
    public bool finishedPlacing = false;
    public bool isSnapping = false;

    GameObject topSphere, bottomSphere;

    Snapping bottomSnap, topSnap;
    RocketMain targetRocketScript;

    public RocketPart part;

    Outline outline;
    bool onMouse = false;

    void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;

        topSphere = transform.GetChild(0).gameObject;
        bottomSphere = transform.GetChild(1).gameObject;

        topSnap = topSphere.GetComponent<Snapping>();
        bottomSnap = bottomSphere.GetComponent<Snapping>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Spawning.spawnedObject = false;
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButtonDown(0) && !finishedPlacing)
        {
            finishedPlacing = true;
            Spawning.spawnedObject = false;
            GetComponent<Renderer>().material = defaultMat;

            //if (!GetComponent<RocketMain>())
            //{
            //    switch (part)
            //    {
            //        case RocketPart.Body:
            //            mainRocketScript = this.gameObject.AddComponent<Engine>();
            //            break;
            //        case RocketPart.Fuel:
            //            break;
            //        case RocketPart.Thruster:
            //            break;
            //    }
            //}
            //else
            //{
            //    mainRocketScript = GetComponent<RocketMain>();
            //}

            //if(!RocketData.rocketData.rocketParent) //if there is no current rocket - add a new one
            //{
            //    GameObject rParent = new("Rocket");
            //    rParent.transform.position = transform.position;
            //    RocketData.rocketData.rocketParent = rParent;
            //    RocketData.rocketData.rocketPartParent = this.gameObject;
            //    this.transform.parent = rParent.transform;
            //    RocketData.rocketData.rocketPartsOrder.Add(this.gameObject);
            //}

            //transform.parent = targetTransform;
        }

        if (!finishedPlacing && !isSnapping)
        {
            Vector3 targetPos;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            targetPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Follow the mouse
            transform.position = targetPos;
        }
    }

    private void OnMouseOver()
    {
        if(finishedPlacing)
        {
            if (!onMouse)
            {
                if (outline == null)
                {
                    outline = gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.green;
                    outline.OutlineWidth = 10f;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (!topSnap.canSnap)
                    {
                        ResetSnapping(topSnap);
                        ResetSnapping(topSnap.targetScript);
                    }

                    if(!bottomSnap.canSnap)
                    {
                        ResetSnapping(bottomSnap);
                        ResetSnapping(bottomSnap.targetScript);
                    }

                    transform.parent = null;
                    Destroy(outline);

                    finishedPlacing = false;
                    onMouse = true;
                }
            }
        }
    }

    void ResetSnapping(Snapping objectToReset)
    {
        objectToReset.canSnap = true;
    }

    private void OnMouseExit()
    {
        if(finishedPlacing)
        {
            Destroy(outline);
            if (onMouse)
            {
                onMouse = false;
            }
        }
    }


    //private void GetClosestPoint()
    //{
    //    topSphere = targetTransform.GetChild(0).gameObject;
    //    bottomSphere = targetTransform.GetChild(1).gameObject;

    //    Vector3 targetTopPosition = targetTransform.position + Vector3.up * (targetTransform.localScale.y * 2f);
    //    Vector3 targetBottomPosition = targetTransform.position - Vector3.up * (targetTransform.localScale.y * 2f);

    //    float checkTopSphere = Vector3.Distance(transform.position, targetTopPosition);
    //    float checkBottomSphere = Vector3.Distance(transform.position, targetBottomPosition);

    //    if (checkTopSphere > checkBottomSphere) //closer to bottom
    //    {
    //        bottomSphere.SetActive(true);
    //        topSphere.SetActive(false);

    //        currentTopSphere.SetActive(true);
    //        currentBottomSphere.SetActive(false);
    //    }
    //    else
    //    {
    //        bottomSphere.SetActive(false);
    //        topSphere.SetActive(true);

    //        currentBottomSphere.SetActive(true);
    //        currentTopSphere.SetActive(false);
    //    }
    //}
}
