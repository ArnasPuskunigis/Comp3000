using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyManager : MonoBehaviour
{
    public SavingSystem saveManager;
    public int money;
    public TMPro.TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        money = saveManager.LoadMoney();
        if (moneyText != null)
        {
            moneyText.text = "$:" + money.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMoney(int newMoney)
    {
        money = newMoney;
        if (moneyText != null)
        {
            moneyText.text = "$:" + money.ToString();
        }
        saveManager.SaveMoney(money);
    }

    public void addToMoney(int newMoney)
    {
        money += newMoney;
        if (moneyText != null)
        {
            moneyText.text = "$:" + money.ToString();
        }
        saveManager.SaveMoney(money);
    }

    public bool checkIfEnough(int cost)
    {
        if(money >= cost)
        {
            money -= cost;
            saveManager.SaveMoney(money);
            if (moneyText != null)
            {
                moneyText.text = "$:" + money.ToString();
            }
            return true;
        }
        return false;
    }

}
