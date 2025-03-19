using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class xpSliderController : MonoBehaviour
{
    public Slider expSlider;
    public exp expScript;

    public TMPro.TextMeshProUGUI levelText;
    public TMPro.TextMeshProUGUI xpText;

    public int fakeLevel;
    public Color[] levelColours; 


    //testing
    public float xpTest;
    void Update()
    {
        
    }
    //here

    void Start()
    {
        setUp();
    }

    public void addExp(float xp)
    {
        expSlider.value = xp;
        expSlider.maxValue = expScript.nextXp;
        xpText.text = Mathf.FloorToInt(xp) + "/" + expScript.nextXp.ToString();
    }

    public void levelUp(float level, float newMax, float xp, float oldMax)
    {
        expSlider.minValue = oldMax;
        expSlider.maxValue = newMax;
        expSlider.value = xp;
        updateUI(level, newMax, xp);
    }


    public void setUp()
    {
        if (expScript.level == 0)
        {
            expSlider.minValue = 0;
        }
        else
        {
            expSlider.minValue = expScript.allLevels[expScript.level - 1];
        }
        expSlider.maxValue = expScript.nextXp;
        int flooredXp = Mathf.FloorToInt(expScript.xp);
        expSlider.value = flooredXp;
        xpText.text = flooredXp + "/" + expScript.nextXp.ToString();
    }

    public  void updateUI(float newLevel, float newMax, float newXp)
    {
        levelText.text = "Level: " + newLevel.ToString();
        int flooredXp = Mathf.FloorToInt(newXp);
        xpText.text = flooredXp.ToString() + "/" + newMax.ToString();
        levelText.color = calcColour();
    }

    public Color calcColour()
    {
        int index = 0;

        if (expScript.level == 0 || expScript.level <= 1)
        {
            index = 0;
        }
        else if (expScript.level == 2|| expScript.level <= 4)
        {
            index = 1;

        }
        else if (expScript.level == 5 || expScript.level <= 9)
        {
            index = 2;

        }
        else if (expScript.level == 10 || expScript.level <= 14)
        {
            index = 3;

        }
        else if (expScript.level == 15 || expScript.level <= 19)
        {
            index = 4;
        }
        else if (expScript.level == 20 || expScript.level <= 29)
        {
            index = 5;
        }
        else if (expScript.level == 30 || expScript.level <= 49)
        {
            index = 6;
        }
        else if (expScript.level == 50 || expScript.level <= 1000000)
        {
            index = 7;
        }
        return levelColours[index];
    }

}
