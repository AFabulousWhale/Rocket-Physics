using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonAssignment : MonoBehaviour
{
    public BodySO rocketBodies;
    string bodyPrefix = "Body";
    VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        FindButtons();
    }

    /// <summary>
    /// gets the name of the buttons and assigns it to the index in the list of objects
    /// </summary>
    void FindButtons()
    {
        for (int i = 0; i < rocketBodies.bodyList.Count; i++) //repeats for the amount of bodies and spawns in the correct one
        {
            Button button = new();
            button = root.Q<Button>($"{bodyPrefix}{i + 1}");
            button.RegisterCallback<ClickEvent, int>(AssignButtonsToObjects, i);
        }
    }

    void AssignButtonsToObjects(ClickEvent evt, int index)
    {
        Instantiate(rocketBodies.bodyList[index].prefab);
    }
}
