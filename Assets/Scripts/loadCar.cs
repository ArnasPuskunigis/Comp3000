using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class loadCar : MonoBehaviour
{
    public GameObject SaveManager;
    public SavingSystem SaveManagerScript;

    public GameObject slowCar;
    public GameObject mediumCar;
    public GameObject fastCar;

    public GameObject slowCarBody;
    public GameObject mediumCarBody;
    public GameObject fastCarBody;

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

    public GameObject slowCarWeaponPistol;
    public GameObject mediumCarWeaponPistol;
    public GameObject fastCarWeaponPistol;

    public GameObject slowCarWeaponSmg;
    public GameObject mediumCarWeaponSmg;
    public GameObject fastCarWeaponSmg;

    public GameObject slowCarWeaponRifle;
    public GameObject mediumCarWeaponRifle;
    public GameObject fastCarWeaponRifle;

    public GameObject slowGunSocket;
    public GameObject mediumGunSocket;
    public GameObject fastGunSocket;

    public Camera mainCamera;
    public GameObject smartCam;

    public CinemachineVirtualCamera virtualCam;

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
            if (SaveManagerScript.carTypeStr == "slow")
            {
                Debug.Log("slow");
                slowCar.SetActive(true);
                ExplodeCar.instance.currentCar = slowCar;
                virtualCam.Follow = slowCar.transform;
                virtualCam.LookAt = slowCar.transform;
                mediumCar.SetActive(false);
                fastCar.SetActive(false);
                currentCar = slowCar;
                currentCarBody = slowCarBody;

            }
            else if (SaveManagerScript.carTypeStr == "medium")
            {

                mediumCar.SetActive(true);
                ExplodeCar.instance.currentCar = mediumCar;
                virtualCam.Follow = mediumCar.transform;
                virtualCam.LookAt = mediumCar.transform;
                slowCar.SetActive(false);
                fastCar.SetActive(false);
                currentCar = mediumCar;
                currentCarBody = mediumCarBody;
            }
            else if (SaveManagerScript.carTypeStr == "fast")
            {
                fastCar.SetActive(true);
                ExplodeCar.instance.currentCar = fastCar;
                virtualCam.Follow = fastCar.transform;
                virtualCam.LookAt = fastCar.transform;
                slowCar.SetActive(false);
                mediumCar.SetActive(false);
                currentCar = fastCar;
                currentCarBody = fastCarBody;
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
                    slowCarWeaponPistol.SetActive(true);
                    slowCarWeaponSmg.SetActive(false);
                    slowCarWeaponRifle.SetActive(false);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    mediumCarWeaponPistol.SetActive(true);
                    mediumCarWeaponSmg.SetActive(false);
                    mediumCarWeaponRifle.SetActive(false);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    fastCarWeaponPistol.SetActive(true);
                    fastCarWeaponSmg.SetActive(false);
                    fastCarWeaponRifle.SetActive(false);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "smg")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    slowCarWeaponPistol.SetActive(false);
                    slowCarWeaponSmg.SetActive(true);
                    slowCarWeaponRifle.SetActive(false);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    mediumCarWeaponPistol.SetActive(false);
                    mediumCarWeaponSmg.SetActive(true);
                    mediumCarWeaponRifle.SetActive(false);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    fastCarWeaponPistol.SetActive(false);
                    fastCarWeaponSmg.SetActive(true);
                    fastCarWeaponRifle.SetActive(false);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "rifle")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    slowCarWeaponPistol.SetActive(false);
                    slowCarWeaponSmg.SetActive(false);
                    slowCarWeaponRifle.SetActive(true);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    mediumCarWeaponPistol.SetActive(false);
                    mediumCarWeaponSmg.SetActive(false);
                    mediumCarWeaponRifle.SetActive(true);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    fastCarWeaponPistol.SetActive(false);
                    fastCarWeaponSmg.SetActive(false);
                    fastCarWeaponRifle.SetActive(true);
                }
            }

            if (SaveManagerScript.carPerkStr == "mag")
            {

            }
            else if (SaveManagerScript.carPerkStr == "xp")
            {

            }
            else if (SaveManagerScript.carPerkStr == "cash")
            {

            }

        }
        else
        {
            mediumCar.SetActive(true);
            virtualCam.Follow = mediumCar.transform;
            virtualCam.LookAt = mediumCar.transform;
            slowCar.SetActive(false);
            fastCar.SetActive(false);
            currentCar = mediumCar;
            currentCarBody = mediumCarBody;

            MeshRenderer carRenderer;
            carRenderer = currentCarBody.GetComponent<MeshRenderer>();
            carRenderer.material = redColour;

            mediumCarWeaponPistol.SetActive(false);
            mediumCarWeaponSmg.SetActive(false);
            mediumCarWeaponRifle.SetActive(true);

        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}
