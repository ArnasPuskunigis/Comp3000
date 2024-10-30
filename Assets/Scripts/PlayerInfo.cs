using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int money;
    public float xp;

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

    public void setMoney(int moneyIn)
    {
        money = moneyIn;
    }

    public void setXp(float xpIn)
    {
        xp = xpIn;
    }

    void Update()
    {

    }
}
