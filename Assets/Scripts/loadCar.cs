using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class loadCar : MonoBehaviour
{
    public SavingSystem SaveManagerScript;
    public Transform spawnPoint;

    public GameObject slowCarPrefab;
    public GameObject mediumCarPrefab;
    public GameObject fastCarPrefab;

    private GameObject currentCar;
    private GameObject currentCarBody;

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

    public GameObject weaponPistolPrefab;
    public GameObject weaponSmgPrefab;
    public GameObject weaponRiflePrefab;

    public Transform slowGunSocket;
    public Transform mediumGunSocket;
    public Transform fastGunSocket;

    private GameObject smartCam;
    private CinemachineVirtualCamera virtualCam;

    private GameObject instance;

    void Start()
    {
        smartCam = GameObject.Find("==== Default Stuff ====/CM vcam1");
        spawnPoint = GameObject.Find("CarSpawnPoint").transform;
        virtualCam = smartCam.GetComponent<CinemachineVirtualCamera>();
        setUpCar();
    }
    public void setUpCar()
    {
        if (SaveManagerScript != null)
        {
            if (SaveManagerScript.carTypeStr == "slow")
            {
                instance = Instantiate(slowCarPrefab, spawnPoint.position, spawnPoint.rotation);
                virtualCam.Follow = instance.transform;
                virtualCam.LookAt = instance.transform;
                currentCar = instance;
                currentCarBody = instance.transform.GetChild(0).gameObject;
                slowGunSocket = instance.transform.GetChild(1);

            }
            else if (SaveManagerScript.carTypeStr == "medium")
            {
                instance = Instantiate(mediumCarPrefab, spawnPoint.position, spawnPoint.rotation);
                virtualCam.Follow = instance.transform;
                virtualCam.LookAt = instance.transform;
                currentCar = instance;
                currentCarBody = instance.transform.GetChild(0).gameObject;
                mediumGunSocket = instance.transform.GetChild(1);
            }
            else if (SaveManagerScript.carTypeStr == "fast")
            {
                instance = Instantiate(fastCarPrefab, spawnPoint.position, spawnPoint.rotation);
                virtualCam.Follow = instance.transform;
                virtualCam.LookAt = instance.transform;
                currentCar = instance;
                currentCarBody = instance.transform.GetChild(0).gameObject;
                fastGunSocket = instance.transform.GetChild(1);
            }

            if (SaveManagerScript.carColourStr == "green")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = greenColour;
            }
            else if (SaveManagerScript.carColourStr == "blue")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = blueColour;
            }
            else if (SaveManagerScript.carColourStr == "yellow")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = yellowColour;
            }
            else if (SaveManagerScript.carColourStr == "red")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = redColour;
            }
            else if (SaveManagerScript.carColourStr == "purple")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = purpleColour;
            }
            else if (SaveManagerScript.carColourStr == "cyan")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = cyanColour;
            }
            else if (SaveManagerScript.carColourStr == "lavender")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = lavenderColour;
            }
            else if (SaveManagerScript.carColourStr == "peach")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = peachColour;
            }
            else if (SaveManagerScript.carColourStr == "pink")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                carRenderer.material = pinkColour;
            }
            else if (SaveManagerScript.carColourStr == "gold")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                Material[] tempMats;
                tempMats = carRenderer.materials;
                tempMats[0] = goldColour;

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
            else if (SaveManagerScript.carColourStr == "silver")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                Material[] tempMats;
                tempMats = carRenderer.materials;
                tempMats[0] = silverColour;

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
            else if (SaveManagerScript.carColourStr == "bronze")
            {
                MeshRenderer carRenderer;
                carRenderer = currentCarBody.GetComponent<MeshRenderer>();
                Material[] tempMats;
                tempMats = carRenderer.materials;
                tempMats[0] = bronzeColour;

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
            //else if (CarInfoComp.carColourStr == "ice")
            //{
            //    MeshRenderer carRenderer;
            //    carRenderer = currentCarBody.GetComponent<MeshRenderer>();
            //    carRenderer.material = pinkColour;
            //}

            if (SaveManagerScript.carWeaponStr == "pistol")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    Instantiate(weaponPistolPrefab, slowGunSocket.position, slowGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponPistolPrefab, mediumGunSocket.position, mediumGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponPistolPrefab, fastGunSocket.position, fastGunSocket.rotation, instance.transform);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "smg")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    Instantiate(weaponSmgPrefab, slowGunSocket.position, slowGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponSmgPrefab, mediumGunSocket.position, mediumGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponSmgPrefab, fastGunSocket.position, fastGunSocket.rotation, instance.transform);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "rifle")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    Instantiate(weaponRiflePrefab, slowGunSocket.position, slowGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponRiflePrefab, mediumGunSocket.position, mediumGunSocket.rotation, instance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponRiflePrefab, fastGunSocket.position, fastGunSocket.rotation, instance.transform);
                }
            }

            //if (SaveManagerScript.carPerkStr == "mag")
            //{

            //}
            //else if (SaveManagerScript.carPerkStr == "xp")
            //{

            //}
            //else if (SaveManagerScript.carPerkStr == "cash")
            //{

            //}

        }
        else
        {
            instance = Instantiate(slowCarPrefab, spawnPoint.position, spawnPoint.rotation);
            virtualCam.Follow = instance.transform;
            virtualCam.LookAt = instance.transform;
            currentCar = instance;
            currentCarBody = instance.transform.GetChild(0).gameObject;

            MeshRenderer carRenderer;
            carRenderer = currentCarBody.GetComponent<MeshRenderer>();
            carRenderer.material = redColour;

            Instantiate(weaponPistolPrefab, slowGunSocket.position, slowGunSocket.rotation);

        }


    }

}
