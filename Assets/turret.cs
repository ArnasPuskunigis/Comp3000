using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class turret : MonoBehaviour
{

    public GameObject target;
    public GameObject turretGun;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float bulletForce;
    public float shotCooldown;
    public float timeElapsed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turretGun.GetComponent<Transform>().LookAt(target.GetComponent<Transform>());
        timeElapsed += Time.deltaTime;
        if (timeElapsed > shotCooldown )
        {
            Instantiate(bullet, bulletSpawnPoint);
            timeElapsed = 0f;
        }
    }

}
