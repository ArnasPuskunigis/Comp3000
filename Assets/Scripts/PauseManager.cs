using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public UnityEngine.UI.Button resumeBtn;
    public UnityEngine.UI.Button exitBtn;

    public bool paused;
    public bool EnableVR;
    // Start is called before the first frame update
    void Start()
    {
        resumeBtn.onClick.AddListener(resumeClicked);
        exitBtn.onClick.AddListener(exitClicked);
        Cursor.visible = false;
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
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Escape))))
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
        }
    }

    public void resumeClicked()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void exitClicked()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
        paused = false;
    }

}
