using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{

    public string carTypeStr;
    public string carColourStr;
    public string carWeaponStr;
    public string carPerkStr;

    public exp xpManager;
    public moneyManager moneyManager;

    private void Awake()
    {
        xpManager.setExp(LoadXp());
        moneyManager.updateMoney(LoadMoney());
        LoadCar();
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
        if (Input.GetKeyDown(KeyCode.N))
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

    public bool LoadRifleUnlock()
    {
        if (PlayerPrefs.GetInt("RifleUnlock") == 0)
        {
            return false;
        }
        return true;
    }

    public void GetLocks()
    {

    }

}
