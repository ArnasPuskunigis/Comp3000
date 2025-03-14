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
    public int reward;
    public bool isDead;
    public Transform temp;

    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("MoneyManager").GetComponent<moneyManager>();
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
            money.addToMoney(reward);
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
