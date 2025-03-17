using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class exp : MonoBehaviour
{
    public SavingSystem saveManager;
    public moneyManager moneyManager;

    public float xp;
    public float nextXp;
    public int level;

    public float[] allLevels;

    //public float targetMultiplierIncrement;
    //public float driftMultiplierIncrement;
    public float lapXpGain;

    public float expMultiplier = 1;

    public float targetsHit;
    public float driftPoints;

    public GameObject lapText;
    public GameObject targetText;
    public GameObject turretText;
    public GameObject goldTargetText;
    public GameObject driftText;
    public GameObject uiSpawner;

    public xpSliderController xpSliderController;
    public int driftExpGain;

    public bool levelInit;

    public AudioClip levelUpSound;
    public AudioClip xpUpSound;

    public ParticleSystem slowLvlUpParticles;
    public ParticleSystem mediumLvlUpParticles;
    public ParticleSystem fastLvlUpParticles;

    public string carType;

    public GameObject levelUpText;

    void Awake()
    {
        nextXp = allLevels[level];
    }

    private void Start()
    {
        if (saveManager.carTypeStr == "slow")
        {
            carType = "slow";
        }
        else if (saveManager.carTypeStr == "medium")
        {
            carType = "medium";
        }
        else if (saveManager.carTypeStr == "fast")
        {
            carType = "fast";
        }
    }

    public void addExp(float xpAmount)
    {
        int currentLevel = level;
        xp += xpAmount;
        checkLevel();
        if (level > currentLevel)
        {
            moneyManager.addToMoney(50);
            int temp = PlayerPrefs.GetInt("CrateCount");
            PlayerPrefs.SetInt("CrateCount", temp + 1);
            PlayerPrefs.Save();
            enableLevelUpText();
            if (carType == "slow")
            {
                slowLvlUpParticles.Play();
            }
            else if (carType == "medium")
            {
                mediumLvlUpParticles.Play();
            }
            else if (carType == "fast")
            {
                fastLvlUpParticles.Play();
            }
        }
        saveManager.SaveXp(xp);
        AudioManager.instance.PlaySfx(xpUpSound, transform, 0.1f);
    }

    public void setExp(float xpAmount)
    {
        levelInit = false;
        xp = xpAmount;
        checkLevel();
        levelInit = true;
    }

    public void checkLevel()
    {
        xpSliderController.addExp(xp);
        if (xp >= nextXp)
        {
            while (xp >= nextXp)
            {
                if (level < allLevels.Length - 2)
                {
                    if (levelInit == true)
                    {
                        AudioManager.instance.PlaySfx(levelUpSound, transform, 1f);
                    }
                    level += 1;
                    nextXp = allLevels[level];
                    xpSliderController.levelUp(level, nextXp, xp, allLevels[level-1]);
                    //Level up rewards
                    //moneyManager.addToMoney(50);
                }
                else
                {
                    level = 999;
                    nextXp = 99999999999999;
                }
            }
        }
    }


    public void lapCompleted()
    {
        Instantiate(lapText, uiSpawner.transform.position, new Quaternion());
        addExp(lapXpGain * expMultiplier);
        // Multiplier implementation
        //expMultiplier = 1 + (targetsHit * targetMultiplierIncrement) + (driftPoints * driftMultiplierIncrement);
        //addExp(lapXpGain * expMultiplier);
        //expMultiplier = 1;
    }

    public void targetHit(float targetValue)
    {
        if (targetValue <= 5)
        {
            Instantiate(targetText, uiSpawner.transform.position, new Quaternion());
        }
        else
        {
            Instantiate(goldTargetText, uiSpawner.transform.position, new Quaternion());
        }
        addExp(targetValue * expMultiplier);
        targetsHit += 1;
    }

    public void turretDestroyed(float value)
    {
        Instantiate(turretText, uiSpawner.transform.position, new Quaternion());
        addExp(value * expMultiplier);
        targetsHit += 1;
    }

    public void addDriftPoints(float input)
    {
        if (input >= 1)
        {
            addExp((input * expMultiplier));
            driftExpGain = (int)((input * expMultiplier));
            Instantiate(driftText, uiSpawner.transform.position, new Quaternion());
            saveManager.addToDriftPoints((int)input);
        }
        //driftPoints += input;
    }

    public void enableLevelUpText()
    {
        levelUpText.SetActive(true);
        Invoke("disableLevelUpText", 1);
    }
    public void disableLevelUpText()
    {
        levelUpText.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
