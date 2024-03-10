using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public FuelSO fuelTanks;
    public BodySO bodies;
    public ThrusterSO thrusters;

    [SerializeField]
    GameObject buttonImagePrefab, hoverTextPrefab;

    [SerializeField]
    Transform bodyAttachment, fuelAttachment, thrusterAttachment;

    Transform canvas;

    public static MainUI UIRef;

    string localPath;

    bool isHovered;
    GameObject label;

    [SerializeField]
    List<GameObject> TABList;
    [SerializeField]
    List<GameObject> TABButtons;

    public MainUI()
    {
        UIRef = this;
    }

    private void Update()
    {
        if(isHovered && label != null)
        {
            label.transform.position = Input.mousePosition;
        }
    }

    public void Start()
    {
        canvas = this.transform;
        InstantiateBodyButton(bodies.bodyList);
        InstantiateFuelButton(fuelTanks.fuelList);
        InstantiateThrusterButton(thrusters.thrusterList);
        TABSetUp();
    }

    #region Bodies
    void InstantiateBodyButton(List<BodyData> partList)
    {
        for (int i = 0; i < partList.Count; i++)
        {
            int index = i; // Create a local variable inside the loop

            GameObject newButton = Instantiate(buttonImagePrefab);
            newButton.transform.parent = bodyAttachment;
            newButton.transform.position = bodyAttachment.position;
            newButton.transform.GetChild(0).GetComponent<Image>().sprite = partList[i].icon;

            //click event
            //GameObject newButton = Instantiate
            EventTrigger eventTrigger = newButton.GetComponent<EventTrigger>();

            // Create a new Entry for the event trigger
            EventTrigger.Entry clickEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            clickEvent.eventID = EventTriggerType.PointerClick;

            // Assign the SpawnPart function to the event
            clickEvent.callback.AddListener((data) => { SpawnBody(partList[index]); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(clickEvent);


            //hover event
            // Create a new Entry for the event trigger
            EventTrigger.Entry hoverEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            hoverEvent.eventID = EventTriggerType.PointerEnter;

            // Assign the SpawnPart function to the event
            hoverEvent.callback.AddListener((data) => { HoverBody(index); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(hoverEvent);


            //unhover event
            UnHoverEvent(eventTrigger);
        }
        bodyAttachment.gameObject.SetActive(false);
    }

    void SpawnBody(BodyData partToSpawn)
    {
        Spawning.SpawnBody(partToSpawn, bodies);
    }

    /// <summary>
    /// called when mouse is hovering over body UI element
    /// </summary>
    /// <param name="index"></param>
    void HoverBody(int index)
    {
        isHovered = true;
        label = Instantiate(hoverTextPrefab);
        label.transform.parent = canvas;

        TextMeshProUGUI massText = InstantiateLabel();

        massText.text = $"Mass Of Part: {bodies.bodyList[index].dryMass}";

        SetMargin(massText);
    }
    #endregion Bodies

    #region Fuel Tanks
    void InstantiateFuelButton(List<FuelData> partList)
    {
        for (int i = 0; i < partList.Count; i++)
        {
            int index = i; // Create a local variable inside the loop

            GameObject newButton = Instantiate(buttonImagePrefab);
            newButton.transform.parent = fuelAttachment;
            newButton.transform.position = fuelAttachment.position;
            newButton.transform.GetChild(0).GetComponent<Image>().sprite = partList[i].icon;

            //click event
            //GameObject newButton = Instantiate
            EventTrigger eventTrigger = newButton.GetComponent<EventTrigger>();

            // Create a new Entry for the event trigger
            EventTrigger.Entry clickEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            clickEvent.eventID = EventTriggerType.PointerClick;

            // Assign the SpawnPart function to the event
            clickEvent.callback.AddListener((data) => { SpawnFuelTank(partList[index]); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(clickEvent);


            //hover event
            // Create a new Entry for the event trigger
            EventTrigger.Entry hoverEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            hoverEvent.eventID = EventTriggerType.PointerEnter;

            // Assign the SpawnPart function to the event
            hoverEvent.callback.AddListener((data) => { HoverFuelTank(index); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(hoverEvent);


            //unhover event
            UnHoverEvent(eventTrigger);
        }
        fuelAttachment.gameObject.SetActive(false);
    }

    void SpawnFuelTank(FuelData partToSpawn)
    {
        Spawning.SpawnFuelTank(partToSpawn, fuelTanks);
    }

    /// <summary>
    /// called when mouse is hovering over fuel UI element
    /// </summary>
    /// <param name="index"></param>
    void HoverFuelTank(int index)
    {
        isHovered = true;
        label = Instantiate(hoverTextPrefab);
        label.transform.parent = canvas;

        TextMeshProUGUI massText = InstantiateLabel();
        TextMeshProUGUI wetMassText = InstantiateLabel();
        TextMeshProUGUI fuelText = InstantiateLabel();

        massText.text = $"Total Mass Of Part: {fuelTanks.fuelList[index].dryMass + fuelTanks.fuelList[index].wetMass}";
        wetMassText.text = $"Wet Mass Of Part: {fuelTanks.fuelList[index].wetMass}";
        fuelText.text = $"Fuel Amount Of Part: {fuelTanks.fuelList[index].fuelAmount}";

        SetMargin(massText);
        SetMargin(wetMassText);
        SetMargin(fuelText);
    }
    #endregion Fuel Tanks

    #region Thrusters
    void InstantiateThrusterButton(List<ThrusterData> partList)
    {
        for (int i = 0; i < partList.Count; i++)
        {
            int index = i; // Create a local variable inside the loop

            GameObject newButton = Instantiate(buttonImagePrefab);
            newButton.transform.parent = thrusterAttachment;
            newButton.transform.position = thrusterAttachment.position;
            newButton.transform.GetChild(0).GetComponent<Image>().sprite = partList[i].icon;

            //click event
            //GameObject newButton = Instantiate
            EventTrigger eventTrigger = newButton.GetComponent<EventTrigger>();

            // Create a new Entry for the event trigger
            EventTrigger.Entry clickEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            clickEvent.eventID = EventTriggerType.PointerClick;

            // Assign the SpawnPart function to the event
            clickEvent.callback.AddListener((data) => { SpawnThruster(partList[index]); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(clickEvent);


            //hover event
            // Create a new Entry for the event trigger
            EventTrigger.Entry hoverEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            hoverEvent.eventID = EventTriggerType.PointerEnter;

            // Assign the SpawnPart function to the event
            hoverEvent.callback.AddListener((data) => { HoverThruster(index); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(hoverEvent);


            //unhover event
            UnHoverEvent(eventTrigger);
        }
        fuelAttachment.gameObject.SetActive(false);
    }

    void SpawnThruster(ThrusterData partToSpawn)
    {
        Spawning.SpawnThruster(partToSpawn, thrusters);
    }

    /// <summary>
    /// called when mouse is hovering over fuel UI element
    /// </summary>
    /// <param name="index"></param>
    void HoverThruster(int index)
    {
        isHovered = true;
        label = Instantiate(hoverTextPrefab);
        label.transform.parent = canvas;

        TextMeshProUGUI massText = InstantiateLabel();
        TextMeshProUGUI thrustText = InstantiateLabel();

        massText.text = $"Mass Of Part: {thrusters.thrusterList[index].dryMass}";
        thrustText.text = $"Thrust Of Part: {thrusters.thrusterList[index].thrustAmount}";

        SetMargin(massText);
        SetMargin(thrustText);
    }
    #endregion Thrusters

    /// <summary>
    /// called when mouse has unhovered from UI element
    /// </summary>
    /// <param name="eventTrigger"></param>
    void UnHoverEvent(EventTrigger eventTrigger)
    {
        isHovered = false;
        EventTrigger.Entry unHoverEvent = new EventTrigger.Entry();

        // Set the event type to PointerClick
        unHoverEvent.eventID = EventTriggerType.PointerExit;

        // Assign the SpawnPart function to the event
        unHoverEvent.callback.AddListener((data) => { DestroyLabel(); });

        // Add the Entry to the EventTrigger's triggers list
        eventTrigger.triggers.Add(unHoverEvent);
    }

    /// <summary>
    /// spawns a new label and return the text to be edited
    /// </summary>
    /// <returns></returns>
    TextMeshProUGUI InstantiateLabel()
    {
        GameObject textGO = new();
        textGO.transform.parent = label.transform;
        return textGO.AddComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// destroys the label when not hovering anymore
    /// </summary>
    void DestroyLabel()
    {
        if(label != null)
        {
            Destroy(label);
        }
    }

    /// <summary>
    /// Sets all the values for the labels
    /// </summary>
    /// <param name="textToSet"></param>
    void SetMargin(TextMeshProUGUI textToSet)
    {
        Vector4 marginSize = new Vector4(textToSet.margin.x, textToSet.margin.y, -161.0684f, textToSet.margin.w);
        textToSet.margin = marginSize;
        textToSet.autoSizeTextContainer = true;
    }

    #region TABS
    void TABSetUp()
    {
        for (int i = 0; i < TABButtons.Count; i++)
        {
            int index = i;

            //GameObject newButton = Instantiate
            EventTrigger eventTrigger = TABButtons[index].AddComponent<EventTrigger>();

            // Create a new Entry for the event trigger
            EventTrigger.Entry clickEvent = new EventTrigger.Entry();

            // Set the event type to PointerClick
            clickEvent.eventID = EventTriggerType.PointerClick;

            // Assign the SpawnPart function to the event
            clickEvent.callback.AddListener((data) => { TABToggle(TABList[index]); });

            // Add the Entry to the EventTrigger's triggers list
            eventTrigger.triggers.Add(clickEvent);
        }
    }
    void TABToggle(GameObject currentSelected)
    {
        Debug.Log(currentSelected.name);
        foreach (var item in TABList)
        {
            if(item == currentSelected)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
    #endregion TABS
    //void SpawnThruster(ClickEvent evt, int index)
    //{
    //    Spawning.SpawnThruster(thrusters.thrusterList[index], thrusters);
    //}

    //public void UpdateUI()
    //{
    //    predictedAltitiude.text = $"Predicted Altitude: {Calculations.calcScriptRef.GetTotalAltitude()}m";
    //    predictedVelocity.text = $"Predicted Velocity: {Calculations.calcScriptRef.GetVelocity()}";
    //    totalMass.text = $"Total Mass: {Calculations.calcScriptRef.GetTotalMass()}g";
    //    totalFuel.text = $"Total Fuel: {Calculations.calcScriptRef.GetFuel()}l";
    //}

    //void LaunchRocket(ClickEvent evt)
    //{
    //    bool prefabSuccess;
    //    PrefabUtility.SaveAsPrefabAsset(RocketData.rocketData.rocketParent, localPath, out prefabSuccess);

    //    if (prefabSuccess)
    //    {
    //        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    //    }
    //}

    //public void TypeDisplayMain(GameObject partInspected, string partType)
    //{

    //    partName.text = partType;

    //    parentElement.Clear();

    //    if(partInspected.GetComponent<Thruster>())
    //    {
    //        Thruster thrusterData = partInspected.GetComponent<Thruster>();
    //        Label massLabel = new();
    //        massLabel.AddToClassList(statStyle);
    //        massLabel.text = $"Mass Of Part: {thrusterData.mass}";

    //        Label thrustLabel = new();
    //        thrustLabel.AddToClassList(statStyle);
    //        thrustLabel.text = $"Thrust Of Part: {thrusterData.thrustAmount}";

    //        parentElement.Add(massLabel);
    //        parentElement.Add(thrustLabel);
    //    }

    //    if(partInspected.GetComponent<Engine>())
    //    {
    //        Engine engineData = partInspected.GetComponent<Engine>();
    //        Label massLabel = new();
    //        massLabel.AddToClassList(statStyle);
    //        massLabel.text = $"Mass Of Part: {engineData.mass}";

    //        parentElement.Add(massLabel);
    //    }

    //    if(partInspected.GetComponent<Fuel>())
    //    {
    //        Fuel fuelData = partInspected.GetComponent<Fuel>();
    //        Label massLabel = new();
    //        massLabel.AddToClassList(statStyle);
    //        massLabel.text = $"Total Mass Of Part: {fuelData.mass}";

    //        Label wetMassLabel = new();
    //        wetMassLabel.AddToClassList(statStyle);
    //        wetMassLabel.text = $"Wet Mass Of Part: {fuelData.wetMass}";

    //        Label fuelLabel = new();
    //        fuelLabel.AddToClassList(statStyle);
    //        fuelLabel.text = $"Fuel Amount Of Part: {fuelData.fuelAmount}";

    //        parentElement.Add(massLabel);
    //        parentElement.Add(wetMassLabel);
    //        parentElement.Add(fuelLabel);
    //    }
    //}

    //public void ThrusterDisplay(MouseOverEvent evt, int index)
    //{
    //    partName.text = thrusters.thrusterList[index].bodyName;

    //    parentElement.Clear();

    //    Label massLabel = new();
    //    massLabel.AddToClassList(statStyle);
    //    massLabel.text = $"Mass Of Part: {thrusters.thrusterList[index].dryMass}";

    //    Label thrustLabel = new();
    //    thrustLabel.AddToClassList(statStyle);
    //    thrustLabel.text = $"Thrust Of Part: {thrusters.thrusterList[index].thrustAmount}";

    //    parentElement.Add(massLabel);
    //    parentElement.Add(thrustLabel);
    //}

    //public void FuelDisplay(MouseOverEvent evt, int index)
    //{
    //    partName.text = fuelTanks.fuelList[index].bodyName;

    //    parentElement.Clear();

    //    Label massLabel = new();
    //    massLabel.AddToClassList(statStyle);
    //    massLabel.text = $"Total Mass Of Part: {fuelTanks.fuelList[index].dryMass + fuelTanks.fuelList[index].wetMass}";

    //    Label wetMassLabel = new();
    //    wetMassLabel.AddToClassList(statStyle);
    //    wetMassLabel.text = $"Wet Mass Of Part: {fuelTanks.fuelList[index].wetMass}";

    //    Label fuelLabel = new();
    //    fuelLabel.AddToClassList(statStyle);
    //    fuelLabel.text = $"Fuel Amount Of Part: {fuelTanks.fuelList[index].fuelAmount}";

    //    parentElement.Add(massLabel);
    //    parentElement.Add(wetMassLabel);
    //    parentElement.Add(fuelLabel);
    //}

    //public void BodyDisplay(MouseOverEvent evt, int index)
    //{
    //    partName.text = bodies.bodyList[index].bodyName;

    //    parentElement.Clear();

    //    Label massLabel = new();
    //    massLabel.AddToClassList(statStyle);
    //    massLabel.text = $"Mass Of Part: {bodies.bodyList[index].dryMass}";

    //    parentElement.Add(massLabel);
    //}
}
