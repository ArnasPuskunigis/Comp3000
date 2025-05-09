using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class loadCar : MonoBehaviour
{
    public GameObject SaveManager;
    public SavingSystem SaveManagerScript;

    public Transform[] spawnPoints;

    public GameObject slowCar;
    public GameObject mediumCar;
    public GameObject fastCar;

    public GameObject currentCar;
    public GameObject currentCarBody;

    public Material redColour;
    public Material blueColour;
    public Material greenColour;
    public Material purpleColour;
    public Material yellowColour;
    public Material cyanColour;
    public Material lavenderColour;
    public Material peachColour;
    public Material pinkColour;
    public Material bronzeColour;
    public Material silverColour;
    public Material goldColour;
    public Material tintWindowsColour;

    public Transform gunSocket;

    public GameObject pistolWeapon;
    public GameObject smgWeapon;
    public GameObject rifleWeapon;

    public Camera mainCamera;
    public GameObject smartCam;
    public CinemachineVirtualCamera virtualCam;


    public int carsSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        setUpCar();
        virtualCam = smartCam.GetComponent<CinemachineVirtualCamera>();
    }

    public void setUpCar()
    {
        if (SaveManagerScript != null)
        {
            SpawnCar();
            SetCarColours();
            SpawnWeapon();
        }
    }

    private void SpawnCar()
    {
        if (SaveManagerScript.carTypeStr == "slow")
        {
            Debug.Log("slow");
            currentCar = Instantiate(slowCar, spawnPoints[carsSpawned]);
            
        }
        else if (SaveManagerScript.carTypeStr == "medium")
        {

            Debug.Log("slow");
            currentCar = Instantiate(mediumCar, spawnPoints[carsSpawned]);
        }
        else if (SaveManagerScript.carTypeStr == "fast")
        {
            Debug.Log("slow");
            currentCar = Instantiate(fastCar, spawnPoints[carsSpawned]);
        }

        currentCarBody = currentCar.transform.GetChild(0).gameObject;
        gunSocket = currentCar.transform.GetChild(1);
        carsSpawned++;
        ExplodeCar.instance.currentCar = currentCar;
        virtualCam.Follow = currentCar.transform;
        virtualCam.LookAt = currentCar.transform;
    }

    private void SetCarColours()
    {
        MeshRenderer carRenderer;
        carRenderer = currentCarBody.GetComponent<MeshRenderer>();
        if (SaveManagerScript.carColourStr == "green")
        {
            carRenderer.material = greenColour;
        }
        else if (SaveManagerScript.carColourStr == "blue")
        {
            carRenderer.material = blueColour;
        }
        else if (SaveManagerScript.carColourStr == "yellow")
        {
            carRenderer.material = yellowColour;
        }
        else if (SaveManagerScript.carColourStr == "red")
        {
            carRenderer.material = redColour;
        }
        else if (SaveManagerScript.carColourStr == "purple")
        {
            carRenderer.material = purpleColour;
        }
        else if (SaveManagerScript.carColourStr == "cyan")
        {
            carRenderer.material = cyanColour;
        }
        else if (SaveManagerScript.carColourStr == "lavender")
        {
            carRenderer.material = lavenderColour;
        }
        else if (SaveManagerScript.carColourStr == "peach")
        {
            carRenderer.material = peachColour;
        }
        else if (SaveManagerScript.carColourStr == "pink")
        {
            carRenderer.material = pinkColour;
        }
        else if (SaveManagerScript.carColourStr == "bronze" || SaveManagerScript.carColourStr == "silver" || SaveManagerScript.carColourStr == "gold")
        {
            Material[] tempMats;
            tempMats = carRenderer.materials;

            if (SaveManagerScript.carColourStr == "bronze") tempMats[0] = goldColour; ;
            if (SaveManagerScript.carColourStr == "silver") tempMats[0] = goldColour; ;
            if (SaveManagerScript.carColourStr == "gold") tempMats[0] = goldColour; ;

            if (SaveManagerScript.carTypeStr == "medium")
            {
                tempMats[5] = tintWindowsColour;
            }
            else
            {
                tempMats[1] = tintWindowsColour;
            }
            carRenderer.materials = tempMats;
        }
    }

    private void SpawnWeapon()
    {
        if (SaveManagerScript.carWeaponStr == "pistol")
        {
            Instantiate(pistolWeapon, gunSocket);
        }
        else if (SaveManagerScript.carWeaponStr == "smg")
        {

            Instantiate(pistolWeapon, gunSocket);
        }
        else if (SaveManagerScript.carWeaponStr == "rifle")
        {

            Instantiate(rifleWeapon, gunSocket);
        }
    }

}
