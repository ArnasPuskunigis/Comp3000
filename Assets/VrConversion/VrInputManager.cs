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
        NavigateToNextSelectable("S");
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) // Simulate Submit
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            Debug.Log("Submit Triggered");
        }

        //if (current == null)
        //{
        //    Selectable next = current.GetComponent<Selectable>()?.FindSelectableOnDown(); // Or use FindSelectableOnUp/Left/Right
        //    if (next != null)
        //    {
        //        next.Select();
        //    }
        //}

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) // Simulate Navigation
        {
            NavigateToNextSelectable("S");
        }

        if (EventSystem.current.currentSelectedGameObject == null ||
        !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
        {
            NavigateToNextSelectable("S");
        }

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)) // Simulate Navigation
        //{
        //    NavigateToNextSelectable("W");
        //}
        //else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)) // Simulate Navigation
        //{
        //    NavigateToNextSelectable("E");
        //}
        //else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)) // Simulate Navigation
        //{
        //    NavigateToNextSelectable("N");
        //}
        //else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)) // Simulate Navigation
        //{
        //    NavigateToNextSelectable("S");
        //}

    }

    void NavigateToNextSelectable(string dir)
    {

        if (current != null)
        {
            Selectable next;

            if (dir == "N")
            {
                next = current.GetComponent<Selectable>()?.FindSelectableOnUp(); // Or use FindSelectableOnUp/Left/Right
            }
            else if (dir == "E")
            {
                next = current.GetComponent<Selectable>()?.FindSelectableOnRight(); // Or use FindSelectableOnUp/Left/Right
            }
            else if (dir == "S")
            {
                next = current.GetComponent<Selectable>()?.FindSelectableOnDown(); // Or use FindSelectableOnUp/Left/Right
            }
            else
            {
                next = current.GetComponent<Selectable>()?.FindSelectableOnLeft(); // Or use FindSelectableOnUp/Left/Right
            }

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
