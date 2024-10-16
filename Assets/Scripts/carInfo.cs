using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carInfo : MonoBehaviour
{
    public string carTypeStr;
    public string carColourStr;
    public string carWeaponStr;
    public string carPerkStr;

    public bool exists;

    // Start is called before the first frame update
    void Start()
    {
        if (!exists)
        {
            DontDestroyOnLoad(gameObject);
            exists = true;
        }
    }

    public void setCarType(string carType)
    {
        carTypeStr = carType;
    }

    public void setColour(string carColour)
    {
        carColourStr = carColour;
    }


    public void setWeapon(string carWeapon)
    {
        carWeaponStr = carWeapon;
    }

    public void setPerk(string carPerk)
    {
        carPerkStr = carPerk;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
