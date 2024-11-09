using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasteryManager : MonoBehaviour
{
    public bool bronzeMasteryUnlocked;
    public bool silverMasteryUnlocked;
    public bool goldMasteryUnlocked;

    //public int bronzeCarMasteryReq;
    //public int silverCarMasteryReq;
    //public int goldCarMasteryReq;

    public int bronzeMasteryReq;
    public int silverMasteryReq;
    public int goldMasteryReq;

    public int totalTargetsHit;
    public int totalDriftPoints;

    public SavingSystem saveManager;
    public loadCar carLoader;

    public GameObject bronzeButton;
    public GameObject silverButton;
    public GameObject goldButton;

    public TMPro.TextMeshProUGUI bronzeProgressText;
    public TMPro.TextMeshProUGUI silverProgressText;
    public TMPro.TextMeshProUGUI goldProgressText;

    public Color red;
    public Color green;

    // Start is called before the first frame update
    void Start()
    {
        bronzeButton.SetActive(false);
        silverButton.SetActive(false);
        goldButton.SetActive(false);

        bronzeProgressText.color = red;
        silverProgressText.color = red;
        goldProgressText.color = red;

        checkMasteryProgress();

        bronzeProgressText.text = totalTargetsHit + " " + "/" + " " + bronzeMasteryReq;
        silverProgressText.text = totalTargetsHit + " " + "/" + " " + silverMasteryReq;
        goldProgressText.text = totalTargetsHit + " " + "/" + " " + goldMasteryReq;

    }

    private void checkMasteryProgress()
    {
        totalTargetsHit = saveManager.loadTargetsHit();
        totalDriftPoints = saveManager.loadDriftPoints();

        if (totalTargetsHit >= goldMasteryReq)
        {
            goldMasteryUnlocked = true;
            silverMasteryUnlocked = true;
            bronzeMasteryUnlocked = true;

            bronzeButton.SetActive(true);
            silverButton.SetActive(true);
            goldButton.SetActive(true);
            bronzeProgressText.color = green;
            silverProgressText.color = green;
            goldProgressText.color = green;
        }
        else if (totalTargetsHit >= silverMasteryReq)
        {
            silverMasteryUnlocked = true;
            bronzeMasteryUnlocked = true;

            bronzeButton.SetActive(true);
            silverButton.SetActive(true);
            bronzeProgressText.color = green;
            silverProgressText.color = green;

        }
        else if (totalTargetsHit >= bronzeMasteryReq)
        {
            bronzeMasteryUnlocked = true;

            bronzeButton.SetActive(true);
            bronzeProgressText.color = green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
