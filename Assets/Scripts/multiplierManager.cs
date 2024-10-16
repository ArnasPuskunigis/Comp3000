using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class multiplierManager : MonoBehaviour
{
    public TextMeshProUGUI multText;

    public bool driftingScore;
    public bool targetScore;

    public exp expScript;

    public float multiplier = 1;

    public void getDrift(float points)
    {
        //multText.text = "MULTIPLIER \n" + (multiplier + (points*expScript.driftMultiplierIncrement)).ToString("F2") + "x";
        //multiplier += (points * expScript.driftMultiplierIncrement);
    }

    public void getTargets(float points)
    {
        //multText.text = "MULTIPLIER \n" + (multiplier + (points * expScript.targetMultiplierIncrement)).ToString("F2") + "x";
        //multiplier += (points * expScript.targetMultiplierIncrement);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
