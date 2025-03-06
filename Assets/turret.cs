using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class turret : MonoBehaviour
{

    public GameObject target;
    public GameObject turretGun;
    public GameObject bullet;
    public Transform[] bulletSpawnPoints;
    public float bulletForce;
    public float shotCooldown;
    public float timeElapsed;
    public GameObject destroy;

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
            int random = Random.Range(0, bulletSpawnPoints.Length);
            Instantiate(bullet, bulletSpawnPoints[random].position, bulletSpawnPoints[random].rotation * Quaternion.Euler(0,-90,0), bulletSpawnPoints[random]);
            timeElapsed = 0f;
        }
    }

}
