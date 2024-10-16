using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShootGun : MonoBehaviour
{
    [SerializeField] private Rigidbody carBody;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject bulletParent;
    [SerializeField] private GameObject bulletParticles;
    public int shootForce;
    public Transform forcePoint;
    public GameObject gun;

    public int ammo;
    public int ammoCapacity;
    public float shootInterval;
    public float shootIntervalTimer;

    public float reloadTime;
    public float reloadTimer;
    public bool reloading;
    public bool hasAmmo;

    public GameObject muzzleFlash;
    public bool isFlashing;

    public TMPro.TextMeshProUGUI ammoText;
    public TMPro.TextMeshProUGUI reloadText;

    // Start is called before the first frame update
    void Start()
    {
        ammo = ammoCapacity;
        ammoText.text = "Ammo: " + ammoCapacity.ToString() + "/" + ammoCapacity.ToString();
        reloadText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (ammo == 0)
            {
                hasAmmo = false;
                reloadText.gameObject.SetActive(true);
            }

            if (hasAmmo && shootIntervalTimer >= shootInterval)
            {
                if (!isFlashing)
                {
                    enableFlash();
                }
                ammo -= 1;
                shootIntervalTimer = 0;
                Vector3 newFoce = new Vector3(0, 0, 0);
                newFoce = gun.transform.forward;

                //Add force to car for the bullet shot
                carBody.AddForceAtPosition(-(newFoce * shootForce), forcePoint.position, ForceMode.Impulse);

                Instantiate(bulletObject, forcePoint.position, forcePoint.rotation, bulletParent.transform);
                GameObject newParticles = Instantiate(bulletParticles, forcePoint.position, forcePoint.rotation, bulletParent.transform);
                newParticles.transform.rotation = Quaternion.LookRotation(newParticles.transform.right);
                ammoText.text = "Ammo: " + ammo.ToString() + "/" + ammoCapacity.ToString();
            }

        }

        if (hasAmmo && shootIntervalTimer < shootInterval && !reloading)
        {
            shootIntervalTimer += Time.deltaTime;
        }

        if (ammo != ammoCapacity && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
            }
        }

        if (reloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                reloadTimer = 0;
                reloading = false;
                ammo = ammoCapacity;
                hasAmmo = true;
                reloadText.gameObject.SetActive(false);
                ammoText.text = "Ammo: " + ammo.ToString() + "/" + ammoCapacity.ToString();
            } 
        }

    }

    public void enableFlash()
    {
        isFlashing = true;
        muzzleFlash.SetActive(true);
        Invoke("disableFlash", 0.1f);
    }

    public void disableFlash()
    {
        isFlashing = false;
        muzzleFlash.SetActive(false);
    }


}
