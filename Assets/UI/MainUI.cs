using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    public FuelSO fuelTanks = default;
    public BodySO bodies = default;
    public ThrusterSO thrusters = default;

    [SerializeField]
    private UIDocument m_UIDocument;

    VisualElement bodyVE, thrusterVE, fuelVE, row, root;
    Button body, thruster, fuel;

    List<VisualElement> menus = new();

    int rowCount = 2;
    const string buttonStyle = "ObjectButton";
    const string rowStyle = "RowClass";

    public static MainUI UIRef;

    Label predictedAltitiude, predictedVelocity, totalMass, totalFuel;

    MainUI()
    {
        UIRef = this;
    }

    public void Start()
    {
        root = m_UIDocument.rootVisualElement; //setting the root element to be the main UI root

        bodyVE = root.Q<VisualElement>("Bodies");
        fuelVE = root.Q<VisualElement>("FuelTanks");
        thrusterVE = root.Q<VisualElement>("Thrusters");

        menus.Add(bodyVE);
        menus.Add(fuelVE);
        menus.Add(thrusterVE);

        body = root.Q<Button>("BodyChoose");
        fuel = root.Q<Button>("FuelTankChoose");
        thruster = root.Q<Button>("ThrusterChoose");

        body.RegisterCallback<ClickEvent, VisualElement>(HideShowMenu, bodyVE);
        fuel.RegisterCallback<ClickEvent, VisualElement>(HideShowMenu, fuelVE);
        thruster.RegisterCallback<ClickEvent, VisualElement>(HideShowMenu, thrusterVE);

        predictedAltitiude = root.Q<Label>("PredictedHeight");
        predictedVelocity = root.Q<Label>("PredictedVelocity");
        totalMass = root.Q<Label>("TotalMass");
        totalFuel = root.Q<Label>("TotalFuel");

        for (int i = 0; i < fuelTanks.fuelList.Count; i++)
        {
            AddRow(fuelVE);
            AddButtons(fuelTanks.fuelList[i], fuelVE, i);
        }

        for (int i = 0; i < bodies.bodyList.Count; i++)
        {
            AddRow(bodyVE);
            AddButtons(bodies.bodyList[i], bodyVE, i);
        }

        for (int i = 0; i < thrusters.thrusterList.Count; i++)
        {
            AddRow(thrusterVE);
            AddButtons(thrusters.thrusterList[i], thrusterVE, i);
        }
    }
    
    void AddButtons(MainRocketData item, VisualElement elementToAttachTo, int index)
    {
        Button button = new(); //creates a new button for each element in the menu based on the max amount of buttons set
        button.name = item.bodyName;
        button.AddToClassList(buttonStyle);
        button.style.backgroundImage = new StyleBackground(item.icon);

        if (elementToAttachTo == bodyVE)
        {
            button.RegisterCallback<ClickEvent, int>(SpawnBody, index);
        }

        if (elementToAttachTo == fuelVE)
        {
            button.RegisterCallback<ClickEvent, int>(SpawnFuel, index);
        }

        if (elementToAttachTo == thrusterVE)
        {
            button.RegisterCallback<ClickEvent, int>(SpawnThruster, index);
        }


        if (row.childCount >= rowCount) //if the current row has reached the desired amount then make a new row
        {
            AddRow(elementToAttachTo);
        }
        row.Add(button); //adding the button to the visual menu
    }

    void AddRow(VisualElement elementToAttachTo)
    {
        row = new();
        elementToAttachTo.Add(row);
        row.AddToClassList(rowStyle);
    }

    void HideShowMenu(ClickEvent evt, VisualElement menuToChange)
    {
        foreach (var item in menus)
        {
            if (item != menuToChange)
            {
                item.style.display = DisplayStyle.None; //hide all other menus
            }
            else
            {
                item.style.display = DisplayStyle.Flex;
            }
        }
    }

    void SpawnBody(ClickEvent evt, int index)
    {
        Spawning.SpawnBody(bodies.bodyList[index], bodies);
    }

    void SpawnFuel(ClickEvent evt, int index)
    {
        Spawning.SpawnFuelTank(fuelTanks.fuelList[index], fuelTanks);
    }

    void SpawnThruster(ClickEvent evt, int index)
    {
        Spawning.SpawnThruster(thrusters.thrusterList[index], thrusters);
    }

    public void UpdateUI()
    {
        predictedAltitiude.text = $"Predicted Altitude: {Calculations.calcScriptRef.GetTotalAltitude()}m";
        predictedVelocity.text = $"Predicted Velocity: {Calculations.calcScriptRef.GetVelocity()}";
        totalMass.text = $"Total Mass: {Calculations.calcScriptRef.GetTotalMass()}g";
        totalFuel.text = $"Total Fuel: {Calculations.calcScriptRef.GetFuel()}l";
    }
}
