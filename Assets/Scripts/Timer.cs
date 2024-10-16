using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float lapTimeSeconds = 0;
    [SerializeField] private float lapTimeMinutes = 0;

    [SerializeField] private TextMeshProUGUI lapTime;
    [SerializeField] private TextMeshProUGUI previousTime;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private GameObject lapTimeObj;
    [SerializeField] private GameObject previousTimeObj;

    private string minutesText;
    private string secondsText;
    private string lapTimeText;

    public float currentLapTime;
    public float bestLapTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTimer();
        lapTimeText = "Lap Time: ";
        minutesText = "";
        secondsText = "";
        lapTimeSeconds = 0;
        lapTimeMinutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentLapTime += Time.deltaTime;
        lapTimeSeconds += Time.deltaTime;
        
        if (lapTimeSeconds >= 60)
        {
            lapTimeSeconds = 0;
            lapTimeMinutes += 1;
            secondsText = "0";
        }

        if (lapTimeSeconds >= 10)
        {
            secondsText = "";
        }

        if (lapTimeMinutes >= 10)
        {
            minutesText = "";
        }

        lapTime.text = lapTimeText + secondsText + lapTimeSeconds.ToString("F3", CultureInfo.InvariantCulture);

        if (lapTimeMinutes > 0)
        {
            secondsText = ":0";
            if (lapTimeSeconds >= 10)
            {
                secondsText = ":";
            }
            lapTime.text = lapTimeText + minutesText + lapTimeMinutes + secondsText + lapTimeSeconds.ToString("F3", CultureInfo.InvariantCulture);
        }

    }

    public void setPreviousTimer(string lapTime)
    {
        previousTime.text = "Previous Lap: " + lapTime.Remove(0, 9);
        previousTimeObj.SetActive(true);
    }

    public void startTimer()
    {
        lapTimeObj.SetActive(true);
    }

    public void resetTimer()
    {
        setPreviousTimer(lapTime.text);
        checkIfBestTime(currentLapTime);

        currentLapTime = 0f;
        lapTimeText = "Lap Time: ";
        minutesText = "";
        secondsText = "";

        lapTimeSeconds = 0;
        lapTimeMinutes = 0;
    }

    public void checkIfBestTime(float lapTime)
    {
        if (lapTime < bestLapTime || bestLapTime == 0f) 
        {
            bestLapTime = lapTime;
            setBestTimer(lapTime);
        }
    }

    public void setBestTimer(float lapTime)
    {
        int minutes = 0;
        float seconds = 0f;

        string secondsTextField = "0";
        string minutesTextField = "0";

        if (lapTime > 60)
        {
            minutes = Mathf.FloorToInt(lapTime / 60);
            seconds = lapTime - (minutes * 60);
            secondsTextField = "0";
        }
        else
        {
            secondsTextField = "";
            seconds = lapTime;
        }

        if (seconds >= 10)
        {
            secondsTextField = "";
        }

        if (minutes >= 10)
        {
            minutesTextField = "";
        }

        string lapTimeString = "";


        lapTimeString = "Best lap: " + secondsText + lapTimeSeconds.ToString("F3", CultureInfo.InvariantCulture);

        if (minutes > 0)
        {
            lapTimeString = "Best lap: " + minutesText + lapTimeMinutes + secondsText + lapTimeSeconds.ToString("F3", CultureInfo.InvariantCulture);
        }

        bestTime.text = lapTimeString;

    }


}
