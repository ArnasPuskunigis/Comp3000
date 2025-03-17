using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int hp;
    public GameObject explosionParticles;
    public GameObject explodedTurret;
    public moneyManager money;
    public exp xpManager;
    public int moneyReward;
    public int xpReward;
    public bool isDead;
    public Transform temp;

    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("MoneyManager").GetComponent<moneyManager>();
        xpManager = GameObject.Find("ExpManager").GetComponent<exp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "bullet")
        {
            hp -= 1;
            CheckHP();
        }
    }

    public void CheckHP()
    {
        if (hp <= 0)
        {
            money.addToMoney(moneyReward);
            xpManager.turretDestroyed(xpReward);
            DestroyTurret();
        }
    }

    public void DestroyTurret()
    {
        Instantiate(explosionParticles, transform.position, transform.rotation, temp);
        Instantiate(explodedTurret, transform.position, transform.rotation, temp);
        isDead = true;
        Destroy(gameObject);
    }

}
