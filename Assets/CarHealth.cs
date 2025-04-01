using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    
    public int hp;
    public int maxHp;
    public TextMeshProUGUI hpText;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        hpText = GameObject.Find("CarUICanvas/CarHp").GetComponent<TextMeshProUGUI>();
        hp = maxHp;
        hpText.text = "HP: " + hp;
    }

    public void DealDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
        }

        hpText.text = "HP: " + hp;
    }

    public void AddHealth(int health)
    {
        hp += health;

        if (hp >= maxHp)
        {
            hp = maxHp;
        }

        hpText.text = "HP: " + hp;
    }

}
