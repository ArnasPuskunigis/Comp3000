using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class driftTextManager : MonoBehaviour
{

    public TextMeshProUGUI driftText;


    public void updateText(float points)
    {
        driftText.gameObject.SetActive(true);
        driftText.text = "DRIFT \n" + (points*100).ToString("F0");
    }

    public void stopText()
    {
        driftText.gameObject.SetActive(false);
        driftText.text = "DRIFT";
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
