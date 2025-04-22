using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine.XR;

public class VrInputManager : MonoBehaviour
{

    //if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) ||

    public GameObject defaultSelectedButton; // Assign your starting button in the Inspector
    public GameObject current;
    public GameObject startBtn;

    public OVRVirtualKeyboard vrKeyboard;
    public TMPro.TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        current = defaultSelectedButton;
        NavigateToNextSelectable("S");
        //vrKeyboard.enabled = false;

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) // Simulate Submit
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            Debug.Log("Submit Triggered");
        }

        //if (OVRInput.GetDown(OVRInput.Button.One)) //a
        //{
        //}

        //if (vrKeyboard != null && vrKeyboard.enabled)
        //{
        //    string typedText = vrKeyboard.TextCommitField.text;
        //    if (inputField != null && typedText != inputField.text)
        //    {
        //        inputField.text = typedText;
        //    }
        //}

        //if (current == null)
        //{
        //    Selectable next = current.GetComponent<Selectable>()?.FindSelectableOnDown(); // Or use FindSelectableOnUp/Left/Right
        //    if (next != null)
        //    {
        //        next.Select();
        //    }
        //}

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) // Simulate Navigation
        //{
        //    NavigateToNextSelectable("S");
        //}

        //if (EventSystem.current.currentSelectedGameObject == null ||
        //!EventSystem.current.currentSelectedGameObject.activeInHierarchy)
        //{
        //    NavigateToNextSelectable("S");
        //}

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)) // Simulate Navigation
        {
            NavigateToNextSelectable("W");
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)) // Simulate Navigation
        {
            NavigateToNextSelectable("E");
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)) // Simulate Navigation
        {
            NavigateToNextSelectable("N");
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)) // Simulate Navigation
        {
            NavigateToNextSelectable("S");
        }

    }

    void NavigateToNextSelectable(string dir)
    {
        if (current != null)
        {
            Selectable currentSelectable = current.GetComponent<Selectable>();
            Selectable next = null;

            switch (dir)
            {
                case "N": next = currentSelectable.FindSelectableOnUp(); break;
                case "E": next = currentSelectable.FindSelectableOnRight(); break;
                case "S": next = currentSelectable.FindSelectableOnDown(); break;
                case "W": next = currentSelectable.FindSelectableOnLeft(); break;
            }

            if (next != null)
            {
                next.Select();
                current = next.gameObject;
                Debug.Log("Navigated to: " + next.name);
            }
            else
            {
                Debug.Log("No selectable found in direction: " + dir);
            }
        }
        else if (defaultSelectedButton != null)
        {
            defaultSelectedButton.GetComponent<Selectable>().Select();
            current = defaultSelectedButton;
            Debug.Log("No selection, defaulting to: " + defaultSelectedButton.name);
        }
    }

    public void SetToStart()
    {
        startBtn.GetComponent<Selectable>().Select();
    }

}
