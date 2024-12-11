using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VrInputManager : MonoBehaviour
{

    //if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) ||

    public GameObject defaultSelectedButton; // Assign your starting button in the Inspector
    public GameObject current;

    // Start is called before the first frame update
    void Start()
    {
        current = defaultSelectedButton;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) // Simulate Submit
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            Debug.Log("Submit Triggered");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) // Simulate Navigation
        {
            NavigateToNextSelectable();
        }
    }

    void NavigateToNextSelectable()
    {

        if (current != null)
        {
            Selectable next = current.GetComponent<Selectable>()?.FindSelectableOnDown(); // Or use FindSelectableOnUp/Left/Right
            if (next != null)
            {
                next.Select();
                Debug.Log("Navigated to: " + next.name);
            }
        }
        else if (defaultSelectedButton != null)
        {
            defaultSelectedButton.GetComponent<Selectable>().Select();
            Debug.Log("No selection, defaulting to: " + defaultSelectedButton.name);
        }
    }
}
