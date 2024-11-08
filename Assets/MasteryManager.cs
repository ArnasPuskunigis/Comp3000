using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MasteryManager : MonoBehaviour
{
    public bool bronzeMasteryUnlocked;
    public bool silverMasteryUnlocked;
    public bool goldMasteryUnlocked;

    public int bronzeMasteryReq;
    public int silverMasteryReq;
    public int goldMasteryReq;

    public int totalTargetsHit;

    public SavingSystem saveManager;

    // Start is called before the first frame update
    void Start()
    {
        checkMasteryProgress();
    }

    private void checkMasteryProgress()
    {
        totalTargetsHit = saveManager.loadTargetsHit();
        if (totalTargetsHit >= goldMasteryReq)
        {
            goldMasteryUnlocked = true;
            silverMasteryUnlocked = true;
            bronzeMasteryUnlocked = true;
        }
        else if (totalTargetsHit >= silverMasteryReq)
        {
            silverMasteryUnlocked = true;
            bronzeMasteryUnlocked = true;
        }
        else if (totalTargetsHit >= bronzeMasteryReq)
        {
            bronzeMasteryUnlocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
