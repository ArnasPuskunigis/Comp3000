using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Audio;

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

    [SerializeField] private AudioClip[] gunShotSfx;
    [SerializeField] private AudioClip reloadSfx;
    [SerializeField] private AudioClip clickSfx;

    public bool EnableVR;

    private CinemachineImpulseSource impulse;

    // Start is called before the first frame update
    void Start()
    {
        ammo = ammoCapacity;
        ammoText.text = "Ammo: " + ammoCapacity.ToString() + "/" + ammoCapacity.ToString();
        reloadText.gameObject.SetActive(false);
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableVR)
        {
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (ammo == 0)
                {
                    AudioManager.instance.PlaySfx(clickSfx, transform, 1f);
                }
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
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

                    AudioManager.instance.PlayRandomSfx(gunShotSfx, transform, 1f);

                    CameraShakeManager.Instance.CameraShake(impulse);

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
                if (OVRInput.Get(OVRInput.Button.Two))
                {
                    reloading = true;
                    AudioManager.instance.PlaySfx(reloadSfx, transform, 0.5f);
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
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (ammo == 0)
                {
                    AudioManager.instance.PlaySfx(clickSfx, transform, 1f);
                }
            }

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

                    AudioManager.instance.PlayRandomSfx(gunShotSfx, transform, 1f);
                    CameraShakeManager.Instance.CameraShake(impulse);
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
                    AudioManager.instance.PlaySfx(reloadSfx, transform, 0.5f);
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
