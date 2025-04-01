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
    public Transform[] bulletSpawnPoints;
    public float bulletForce;
    public float shotCooldown;
    public float timeElapsed;
    public Health turretHp;
    public loadCar carLoader;
    public bool carFound;

    public bool inRange;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        turretHp = GetComponent<Health>();
        carLoader = GameObject.Find("CarLoader").GetComponent<loadCar>();
        if (carLoader.currentCar != null)
        {
            target = carLoader.currentCar;
            carFound = true;
        }
        else
        {
            carFound = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carFound)
        {
            checkIfInRange();

            timeElapsed += Time.deltaTime;
            if (timeElapsed > shotCooldown && turretHp.hp > 0 && inRange)
            {
                shoot();
            }
        }

        else if (!carFound && carLoader.currentCar != null) 
        {
            target = carLoader.currentCar;
            carFound= true;
        }
    }

    public void checkIfInRange()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            inRange = true;
            turretGun.GetComponent<Transform>().LookAt(target.GetComponent<Transform>());
        }
        else
        {
            inRange = false;
        }
    }

    public void shoot()
    {
        int random = Random.Range(0, bulletSpawnPoints.Length);
        Instantiate(bullet, bulletSpawnPoints[random].position, bulletSpawnPoints[random].rotation * Quaternion.Euler(0, -90, 0));
        timeElapsed = 0f;
    }



}
