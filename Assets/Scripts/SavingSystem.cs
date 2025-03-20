using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{

    public string carTypeStr;
    public string carColourStr;
    public string carWeaponStr;
    public string carPerkStr;

    public int totalTargetsHit;
    public int totalDriftPoints;

    public exp xpManager;
    public moneyManager moneyManager;
    public mainUiManager uiManager;

    public bool xpInitialised;

    private void Awake()
    {
        moneyManager.updateMoney(LoadMoney());
        LoadCar();
        totalTargetsHit = loadTargetsHit();
        totalDriftPoints = loadDriftPoints();
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    resetRareColours();
        //}
    }

    public void resetRareColours()
    {
        PlayerPrefs.SetInt("CyanUnlock", 0);
        PlayerPrefs.SetInt("LavenderUnlock", 0);
        PlayerPrefs.SetInt("PeachUnlock", 0);
        PlayerPrefs.SetInt("PinkUnlock", 0);
        PlayerPrefs.Save();
        uiManager.checkForUnlocks();
    }

    public int loadTargetsHit()
    {
        int temp = PlayerPrefs.GetInt("TotalTargetsHit");
        return temp;
    }

    public int loadDriftPoints()
    {
        int temp = PlayerPrefs.GetInt("TotalDriftPoints");
        return temp;
    }

    public void addToTargetsHit()
    {
        totalTargetsHit++;
        PlayerPrefs.SetInt("TotalTargetsHit", totalTargetsHit);
        PlayerPrefs.Save();
    }

    public void addToDriftPoints(int driftPoints)
    {
        totalDriftPoints += driftPoints;
        PlayerPrefs.SetInt("TotalDriftPoints", totalDriftPoints);
        PlayerPrefs.Save();
    }

    public void SaveMoney(int money)
    {
        PlayerPrefs.SetInt("PlayerMoney", money);
        PlayerPrefs.Save();
    }

    public void SaveXp(float xp)
    {
        PlayerPrefs.SetFloat("PlayerXp", xp);
        PlayerPrefs.Save();
    }

    public void SaveCar(string carType, string carColourStr, string carWeaponStr, string carPerkStr)
    {
        PlayerPrefs.SetString("CarType", carType);
        PlayerPrefs.SetString("CarColour", carColourStr);
        PlayerPrefs.SetString("CarWeapon", carWeaponStr);
        PlayerPrefs.SetString("CarPerk", carPerkStr);
        PlayerPrefs.Save();
    }

    public int LoadMoney()
    {
        int money = PlayerPrefs.GetInt("PlayerMoney");
        return money;
    }

    public float LoadXp()
    {
        float xp = PlayerPrefs.GetFloat("PlayerXp");
        return xp;
    }

    public void LoadCar()
    {
        carTypeStr = PlayerPrefs.GetString("CarType");
        carColourStr = PlayerPrefs.GetString("CarColour");
        carWeaponStr = PlayerPrefs.GetString("CarWeapon");
        carPerkStr = PlayerPrefs.GetString("CarPerk");
    }

    private void Update()
    {
        if (xpManager.levelsInitialised && !xpInitialised)
        {
            xpManager.setExp(LoadXp());
            xpInitialised = true;
        }

        if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.RightControl))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SaveMediumCarUnlock()
    {
        PlayerPrefs.SetInt("MediumCarUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SaveFastCarUnlock()
    {
        PlayerPrefs.SetInt("FastCarUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SaveSmgUnlock()
    {
        PlayerPrefs.SetInt("SmgUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SaveRifleUnlock()
    {
        PlayerPrefs.SetInt("RifleUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SaveCyanUnlock()
    {
        PlayerPrefs.SetInt("CyanUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SaveLavenderUnlock()
    {
        PlayerPrefs.SetInt("LavenderUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SavePeachUnlock()
    {
        PlayerPrefs.SetInt("PeachUnlock", 1);
        PlayerPrefs.Save();
    }

    public void SavePinkUnlock()
    {
        PlayerPrefs.SetInt("PinkUnlock", 1);
        PlayerPrefs.Save();
    }

    public bool LoadMediumCarUnlock()
    {
        if (PlayerPrefs.GetInt("MediumCarUnlock") == 0)
        {
            return false;
        }
        return true;
    }

    public bool LoadFastCarUnlock()
    {
        if (PlayerPrefs.GetInt("FastCarUnlock") == 0)
        {
            return false;
        }
        return true;
    }

    public bool LoadSmgUnlock()
    {
        if (PlayerPrefs.GetInt("SmgUnlock") == 0)
        {
            return false;
        }
        return true;
    }
    public bool LoadCyanUnlock()
    {
        if (PlayerPrefs.GetInt("CyanUnlock") == 0)
        {
            return false;
        }
        return true;
    }

    public bool LoadLavenderUnlock()
    {
        if (PlayerPrefs.GetInt("LavenderUnlock") == 0)
        {
            return false;
        }
        return true;
    }
    public bool LoadPeachUnlock()
    {
        if (PlayerPrefs.GetInt("PeachUnlock") == 0)
        {
            return false;
        }
        return true;
    }
    public bool LoadPinkUnlock()
    {
        if (PlayerPrefs.GetInt("PinkUnlock") == 0)
        {
            return false;
        }
        return true;
    }

    public bool LoadRifleUnlock()
    {
        if (PlayerPrefs.GetInt("RifleUnlock") == 0)
        {
            return false;
        }
        return true;
    }



}
