using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;
using Unity.Networking;

public class loadCar : NetworkBehaviour
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

    public Transform gunSocket;

    private GameObject smartCam;
    private CinemachineVirtualCamera virtualCam;

    private GameObject carInstance;


    void Start()
    {
        spawnPoint = GameObject.Find("CarSpawnPoint").transform;
        setUpCar();
    }

    public void RequestSpawnCar()
    {
        if (IsOwner) // Ensures only the player who owns the object requests spawning
        {
            SpawnCarServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnCarServerRpc(ulong clientId)
    {
        if (IsServer)
        {
            GameObject carPrefab = GetCarPrefabFromSaveData(); // Get correct car prefab
            carInstance = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

            carInstance.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        }
    }

    private GameObject GetCarPrefabFromSaveData()
    {
        if (SaveManagerScript.carTypeStr == "slow")
            return slowCarPrefab;
        if (SaveManagerScript.carTypeStr == "medium")
            return mediumCarPrefab;
        if (SaveManagerScript.carTypeStr == "fast")
            return fastCarPrefab;

        return slowCarPrefab; // Default to slow car
    }

    public void setUpCar()
    {
        if (SaveManagerScript != null)
        {

            if (IsOwner)
            {
                RequestSpawnCar();
            }

            smartCam = GameObject.Find("==== Default Stuff ====/CM vcam1");
            virtualCam = smartCam.GetComponent<CinemachineVirtualCamera>();
            virtualCam.Follow = carInstance.transform;
            virtualCam.LookAt = carInstance.transform;
            currentCar = carInstance;
            currentCarBody = carInstance.transform.GetChild(0).gameObject;
            gunSocket = carInstance.transform.GetChild(1);

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
                    Instantiate(weaponPistolPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponPistolPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponPistolPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "smg")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    Instantiate(weaponSmgPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponSmgPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponSmgPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
            }
            else if (SaveManagerScript.carWeaponStr == "rifle")
            {

                if (SaveManagerScript.carTypeStr == "slow")
                {
                    Instantiate(weaponRiflePrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "medium")
                {
                    Instantiate(weaponRiflePrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
                }
                if (SaveManagerScript.carTypeStr == "fast")
                {
                    Instantiate(weaponRiflePrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);
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
            carInstance = Instantiate(slowCarPrefab, spawnPoint.position, spawnPoint.rotation);
            virtualCam.Follow = carInstance.transform;
            virtualCam.LookAt = carInstance.transform;
            currentCar = carInstance;
            currentCarBody = carInstance.transform.GetChild(0).gameObject;

            MeshRenderer carRenderer;
            carRenderer = currentCarBody.GetComponent<MeshRenderer>();
            carRenderer.material = redColour;

            Instantiate(weaponPistolPrefab, gunSocket.position, gunSocket.rotation, carInstance.transform);

        }




    }
}
