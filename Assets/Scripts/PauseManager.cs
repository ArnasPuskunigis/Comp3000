using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public UnityEngine.UI.Button resumeBtn;
    public UnityEngine.UI.Button exitBtn;

    public bool paused;
    public bool EnableVR;

    public GameObject defaultSelectedButton; // Assign your starting button in the Inspector
    public GameObject current;

    public bool multiplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        resumeBtn.onClick.AddListener(resumeClicked);
        exitBtn.onClick.AddListener(exitClicked);
        if (multiplayer)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        current = defaultSelectedButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableVR)
        {
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                if (paused)
                {
                    pauseMenu.SetActive(false);
                    paused = false;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.visible = true;
                    pauseMenu.SetActive(true);
                    paused = true;
                }
            }

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
        else
        {
            if ((Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Escape))))
            {
                if (paused)
                {
                    pauseMenu.SetActive(false);
                    paused = false;
                    if (!multiplayer)
                    {
                        Cursor.visible = false;
                    }
                    else
                    {
                        Cursor.visible = true;
                    }
                }
                else
                {
                    Cursor.visible = true;
                    pauseMenu.SetActive(true);
                    paused = true;
                }
            }
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

    public void resumeClicked()
    {
        if (!multiplayer)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void exitClicked()
    {
        Cursor.visible = true;

        if (multiplayer)
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.Shutdown();
            }
            else if (NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsConnectedClient)
            {
                NetworkManager.Singleton.Shutdown();
            }
        }

        SceneManager.LoadScene("MainMenu");
        paused = false;

    }

}
