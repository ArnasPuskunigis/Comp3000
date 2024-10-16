using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapUI : MonoBehaviour
{
    public TextMeshProUGUI lapText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToLap(int lap)
    {
        lapText.text = lap + "/3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
