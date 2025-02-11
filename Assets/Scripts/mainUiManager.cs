using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System.Runtime.Serialization.Formatters;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Drawing;
public class mainUiManager : MonoBehaviour
{
    public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Button garageButton;
    public UnityEngine.UI.Button exitButton;

    public GameObject mainCamera;
    public RectTransform xpSlider;
    public GameObject xpSliderObj;
    public GameObject fastCarWeaponSocket;
    public GameObject mediumCarWeaponSocket;
    public GameObject slowCarWeaponSocket;
    
    public GameObject playMenuItems;
    public GameObject garageMenuItems;
    public GameObject mainMenuItems;
    public GameObject cratesMenuItems;

    public GameObject bodyMenuItems;
    public GameObject weaponMenuItems;
    public GameObject paintMenuItems;
    public GameObject perkMenuItems;
    public GameObject masteryMenuItems;

    public UnityEngine.UI.Button playBackButton;
    public UnityEngine.UI.Button garageBackButton;

    public UnityEngine.UI.Button bodyButton;
    public UnityEngine.UI.Button weaponButton;
    public UnityEngine.UI.Button paintButton;
    public UnityEngine.UI.Button perkButton;

    public GameObject currentCar;
    public Material currentColour;

    public UnityEngine.UI.Button[] carColourButtons;
    public UnityEngine.UI.Button[] gunColourButtons;
    public UnityEngine.UI.Button tempColorBtn;

    public Material[] carMaterials;

    public Material defaultWindowsMat;
    public MeshFilter fastDefaultMeshFilter;
    public MeshRenderer fastDefaultMeshRenderer;
    public MeshFilter fastIceMeshFilter;
    public MeshRenderer fastIceMeshRenderer;
    
    public Material[] gunMaterials;

    public Material newestMat;

    public UnityEngine.UI.Button slowCarBtn;
    public UnityEngine.UI.Button mediumCarBtn;
    public UnityEngine.UI.Button fastCarBtn;

    public GameObject slowCarObj;
    public GameObject mediumCarObj;
    public GameObject fastCarObj;

    public GameObject slowCarBody;
    public GameObject mediumCarBody;
    public GameObject fastCarBody;

    public string currentGunStr;
    public string currentCarStr;
    public string currentColourStr;
    public string currentPerkStr;

    public GameObject scPistol;
    public GameObject scSmg;
    public GameObject scRifle;

    public GameObject mcPistol;
    public GameObject mcSmg;
    public GameObject mcRifle;

    public GameObject fcPistol;
    public GameObject fcSmg;
    public GameObject fcRifle;

    public GameObject maggedPerkBtn;
    public GameObject intelectPerkBtn;
    public GameObject richesPerkBtn;

    public GameObject maggedPerkUnlock;
    public GameObject intelectPerkUnlock;
    public GameObject richesPerkUnlock;

    public GameObject mediumUnlock;
    public GameObject fastUnlock;

    public GameObject smgUnlock;
    public GameObject rifleUnlock;

    public UnityEngine.UI.Button maggedPerkUnlockBtn;
    public UnityEngine.UI.Button intelectPerkUnlockBtn;
    public UnityEngine.UI.Button richesPerkUnlockBtn;

    public UnityEngine.UI.Button smgUnlockBtn;
    public UnityEngine.UI.Button rifleUnlockBtn;

    public UnityEngine.UI.Button pistolBtn;
    public UnityEngine.UI.Button smgBtn;
    public UnityEngine.UI.Button arBtn;

    public UnityEngine.UI.Button cyanBtn;
    public UnityEngine.UI.Button lavenderBtn;
    public UnityEngine.UI.Button peachBtn;
    public UnityEngine.UI.Button pinkBtn;

    public UnityEngine.UI.Button maggedBtn;
    public UnityEngine.UI.Button intelectBtn;
    public UnityEngine.UI.Button richesBtn;

    public bool maggedUnlocked;
    public bool intelectUnlocked;
    public bool richesUnlocked;

    public int maggedPrice;
    public int intelectPrice;
    public int richesPrice;

    public int mediumPrice;
    public int fastPrice;

    public int smgPrice;
    public int riflePrice;

    public bool mediumUnlocked;
    public bool fastUnlocked;

    public bool smgUnlocked;
    public bool rifleUnlocked;

    public bool cyanUnlocked;
    public bool lavenderUnlocked;
    public bool peachUnlocked;
    public bool pinkUnlocked;

    public SavingSystem saveManager;
    public UnityEngine.UI.Button chaosBtn;
    public UnityEngine.UI.Button desertBtn;
    public UnityEngine.UI.Button cityBtn;
    public moneyManager theMoneyManager;

    public UnityEngine.Color normalBtnColour;
    public UnityEngine.Color greenBtnColour;

    ColorBlock bodyButtonCB;
    ColorBlock weaponButtonCB;
    ColorBlock paintButtonCB;
    ColorBlock perkButtonCB;

    public openCrate crateManager;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playPressed);
        garageButton.onClick.AddListener(garagePressed);
        exitButton.onClick.AddListener(exitPressed);
        playBackButton.onClick.AddListener(playBackPressed);
        garageBackButton.onClick.AddListener(garageBackPressed);
        playMenuItems.SetActive(false);
        garageMenuItems.SetActive(false);
        xpSlider.localPosition = new Vector3(0f, -405f, 0f);

        bodyButton.onClick.AddListener(bodyButtonPressed);
        weaponButton.onClick.AddListener(weaponButtonPressed);
        paintButton.onClick.AddListener(paintButtonPressed);
        perkButton.onClick.AddListener(perkButtonPressed);

        checkForUnlocks();

        if (saveManager.carTypeStr != "" && saveManager.carWeaponStr != "" && saveManager.carColourStr != "")
        {
            currentCarStr = saveManager.carTypeStr;
            currentGunStr = saveManager.carWeaponStr;
            currentColourStr = saveManager.carColourStr;

            if (currentCarStr == "slow")
            {
                slowCarPressed();
            }
            else if (currentCarStr == "medium")
            {
                mediumCarPressed();
            }
            else if (currentCarStr == "fast")
            {
                fastCarPressed();
            }

            if (currentGunStr == "pistol")
            {
                pistolPressed();
            }
            else if (currentGunStr == "smg")
            {
                smgPressed();
            }
            else if (currentGunStr == "rifle")
            {
                riflePressed();
            }
            carColourPressed(currentColourStr);
            currentPerkStr = "";
        }
        else
        {
            slowCarPressed();
            pistolPressed();
            carColourPressed("red");
            currentPerkStr = "";
        }

        bodyButtonCB = bodyButton.colors;
        weaponButtonCB = weaponButton.colors;
        paintButtonCB = paintButton.colors;
        perkButtonCB = perkButton.colors;

        mainCamera.transform.localPosition = new Vector3(134, 24, -81);
        mainCamera.transform.eulerAngles = new Vector3(24f, 248f, 0f);
    }

    public void checkForUnlocks()
    {
        if (saveManager.LoadMediumCarUnlock())
        {
            mediumUnlocked = true;
            mediumUnlock.SetActive(false);
            mediumCarBtn.interactable = true;
        }
        if (saveManager.LoadFastCarUnlock())
        {
            fastUnlocked = true;
            fastUnlock.SetActive(false);
            fastCarBtn.interactable = true;
        }
        if (saveManager.LoadSmgUnlock())
        {
            smgUnlocked = true;
            smgUnlock.SetActive(false);
            smgBtn.interactable = true;
        }
        if (saveManager.LoadRifleUnlock())
        {
            rifleUnlocked = true;
            rifleUnlock.SetActive(false);
            arBtn.interactable = true;
        }

        if (saveManager.LoadCyanUnlock())
        {
            cyanUnlocked = true;
            cyanBtn.interactable = true;
        }

        if (saveManager.LoadLavenderUnlock())
        {
            lavenderUnlocked = true;
            lavenderBtn.interactable = true;
        }

        if (saveManager.LoadPeachUnlock())
        {
            peachUnlocked = true;
            peachBtn.interactable = true;
        }

        if (saveManager.LoadPinkUnlock())
        {
            pinkUnlocked = true;
            pinkBtn.interactable = true;
        }

        //if (maggedUnlocked)
        //{
        //    maggedPerkUnlock.SetActive(false);
        //    maggedBtn.interactable = true;
        //}
        //if (intelectUnlocked)
        //{
        //    intelectPerkUnlock.SetActive(false);
        //    intelectBtn.interactable = true;
        //}
        //if (richesUnlocked)
        //{
        //    richesPerkUnlock.SetActive(false);
        //    richesBtn.interactable = true;
        //}
    }

    public void resetCarVisuals()
    {
        slowCarObj.SetActive(false);
        mediumCarObj.SetActive(false);
        fastCarObj.SetActive(false);

        if (currentCarStr == "slow")
        {
            slowCarObj.SetActive(true);
        }
        else if (currentCarStr == "medium")
        {
            mediumCarObj.SetActive(true);
        }
        else if (currentCarStr == "fast")
        {
            fastCarObj.SetActive(true);
        }

    }

    public void resetWeaponVisuals()
    {

        scPistol.SetActive(false);
        scSmg.SetActive(false);
        scRifle.SetActive(false);

        mcPistol.SetActive(false);
        mcSmg.SetActive(false);
        mcRifle.SetActive(false);

        fcPistol.SetActive(false);
        fcSmg.SetActive(false);
        fcRifle.SetActive(false);


        if (currentGunStr == "pistol")
        {
            scPistol.SetActive(true);
            mcPistol.SetActive(true);
            fcPistol.SetActive(true);
        }
        else if (currentGunStr == "smg")
        {
            scSmg.SetActive(true);
            mcSmg.SetActive(true);
            fcSmg.SetActive(true);
        }
        else if (currentGunStr == "rifle")
        {
            scRifle.SetActive(true);
            mcRifle.SetActive(true);
            fcRifle.SetActive(true);
        }

    }


    public void cratesPressed()
    {
        crateManager.crateCount = PlayerPrefs.GetInt("CrateCount");
        cratesMenuItems.SetActive(true);
        mainMenuItems.SetActive(false);
        mainCamera.transform.localPosition = new Vector3(104f, 1.49f, -117f);
        mainCamera.transform.eulerAngles = new Vector3(33.5f, 274.16f, 0f);
    }

    public void cratesBackPressed()
    {
        cratesMenuItems.SetActive(false);
        mainMenuItems.SetActive(true);
        mainCamera.transform.localPosition = new Vector3(134, 24, -81);
        mainCamera.transform.eulerAngles = new Vector3(24f, 248f, 0f);
    }

    //public void chaosBtnPressed()
    //{
    //    SceneManager.LoadScene(1);
    //}

    public void desertBtnPressed()
    {
        SceneManager.LoadScene(3);
    }

    public void cityBtnPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void volcanoBtnPressed()
    {
        SceneManager.LoadScene(4);
    }

    public void playPressed()
    {
        playMenuItems.SetActive(true);
        mainMenuItems.SetActive(false);
    }

    public void masteryPressed()
    {
        masteryMenuItems.SetActive(true);
        mainMenuItems.SetActive(false);
    }

    public void masteryBackPressed()
    {
        masteryMenuItems.SetActive(false);
        mainMenuItems.SetActive(true);
    }

    public void garagePressed()
    {
        if (bodyButton != null)
        {
            EventSystem.current.SetSelectedGameObject(bodyButton.gameObject, new BaseEventData(EventSystem.current));
        }
        mainCamera.transform.localPosition = new Vector3(134, 24, -81);
        mainCamera.transform.eulerAngles = new Vector3(24f, 248f, 0f);
        garageMenuItems.SetActive(true);
        bodyMenuItems.SetActive(true);
        paintMenuItems.SetActive(false);
        weaponMenuItems.SetActive(false);
        perkMenuItems.SetActive(false);
        mainMenuItems.SetActive(false);
    }

    public void exitPressed()
    {
        Application.Quit();
    }

    public void playBackPressed()
    {
        playMenuItems.SetActive(false);
        mainMenuItems.SetActive(true);
    }

    public void garageBackPressed()
    {
        resetCarVisuals();
        resetWeaponVisuals();
        garageMenuItems.SetActive(false);
        mainMenuItems.SetActive(true);

        bodyButtonCB.normalColor = normalBtnColour;
        bodyButton.colors = bodyButtonCB;

        weaponButtonCB.normalColor = normalBtnColour;
        weaponButton.colors = weaponButtonCB;

        paintButtonCB.normalColor = normalBtnColour;
        paintButton.colors = paintButtonCB;

        perkButtonCB.normalColor = normalBtnColour;
        perkButton.colors = perkButtonCB;
        
        //xpSlider.localPosition = new Vector3(0f, -374f, 0f);
    }

    public void bodyButtonPressed()
    {
        resetCarVisuals();
        resetWeaponVisuals();
        xpSliderObj.SetActive(true);
        bodyMenuItems.SetActive(true);
        paintMenuItems.SetActive(false);
        weaponMenuItems.SetActive(false);
        perkMenuItems.SetActive(false);
        garageMenuItems.SetActive(true);
        //mainCamera.transform.eulerAngles = new Vector3(15f, 235f, 0f);

        bodyButtonCB.normalColor = greenBtnColour;
        bodyButton.colors = bodyButtonCB;

        weaponButtonCB.normalColor = normalBtnColour;
        weaponButton.colors = weaponButtonCB;

        paintButtonCB.normalColor = normalBtnColour;
        paintButton.colors = paintButtonCB;

        perkButtonCB.normalColor = normalBtnColour;
        perkButton.colors = perkButtonCB;

        


    }

    public void weaponButtonPressed()
    {
        resetCarVisuals();
        resetWeaponVisuals();
        xpSliderObj.SetActive(true);
        bodyMenuItems.SetActive(false);
        paintMenuItems.SetActive(false);
        weaponMenuItems.SetActive(true);
        perkMenuItems.SetActive(false);
        garageMenuItems.SetActive(true);
        //mainCamera.transform.eulerAngles = new Vector3(15f, 235f, 0f);

        bodyButtonCB.normalColor = normalBtnColour;
        bodyButton.colors = bodyButtonCB;

        weaponButtonCB.normalColor = greenBtnColour;
        weaponButton.colors = weaponButtonCB;

        paintButtonCB.normalColor = normalBtnColour;
        paintButton.colors = paintButtonCB;

        perkButtonCB.normalColor = normalBtnColour;
        perkButton.colors = perkButtonCB;
    }

    public void perkButtonPressed()
    {
        resetCarVisuals();
        resetWeaponVisuals();
        xpSliderObj.SetActive(true);
        bodyMenuItems.SetActive(false);
        paintMenuItems.SetActive(false);
        weaponMenuItems.SetActive(false);
        perkMenuItems.SetActive(true);
        garageMenuItems.SetActive(true);
        //mainCamera.transform.eulerAngles = new Vector3(15f, 235f, 0f);

        bodyButtonCB.normalColor = normalBtnColour;
        bodyButton.colors = bodyButtonCB;

        weaponButtonCB.normalColor = normalBtnColour;
        weaponButton.colors = weaponButtonCB;

        paintButtonCB.normalColor = normalBtnColour;
        paintButton.colors = paintButtonCB;

        perkButtonCB.normalColor = greenBtnColour;
        perkButton.colors = perkButtonCB;
    }

    public void paintButtonPressed()
    {
        resetCarVisuals();
        resetWeaponVisuals();
        xpSliderObj.SetActive(true);
        bodyMenuItems.SetActive(false);
        paintMenuItems.SetActive(true);
        weaponMenuItems.SetActive(false);
        perkMenuItems.SetActive(false);
        garageMenuItems.SetActive(true);
        //mainCamera.transform.eulerAngles = new Vector3(15f, 235f, 0f);

        bodyButtonCB.normalColor = normalBtnColour;
        bodyButton.colors = bodyButtonCB;

        weaponButtonCB.normalColor = normalBtnColour;
        weaponButton.colors = weaponButtonCB;

        paintButtonCB.normalColor = greenBtnColour;
        paintButton.colors = paintButtonCB;

        perkButtonCB.normalColor = normalBtnColour;
        perkButton.colors = perkButtonCB;
    }

    public void carColourPressed(string color)
    {
        MeshRenderer carRenderer;
        int key = 0;
        if (color == "red")
        {
            key = 0;
        }
        else if (color == "yellow")
        {
            key = 1;
        }
        else if (color == "green")
        {
            key = 2;
        }
        else if (color == "blue")
        {
            key = 3;
        }
        else if (color == "purple")
        {
            key = 4;
        }
        else if (color == "cyan")
        {
            key = 5;
        }
        else if (color == "lavender")
        {
            key = 6;
        }
        else if (color == "peach")
        {
            key = 7;
        }
        else if (color == "pink")
        {
            key = 8;
        }
        else if (color == "bronze")
        {
            key = 9;
        }
        else if (color == "silver")
        {
            key = 10;
        }
        else if (color == "gold")
        {
            key = 11;
        }
        else if (color == "ice")
        {
            key = 12;
        }

        for (int i = 0; i < carColourButtons.Length; i++)
        {
            if (i == key)
            {
                if (key == 12)
                {
                    MeshFilter carMeshFilter = currentCar.GetComponent<MeshFilter>();
                    MeshRenderer carMeshRenderer = currentCar.GetComponent<MeshRenderer>();
                    carMeshFilter.mesh = fastIceMeshFilter.sharedMesh;
                    carMeshRenderer.materials = fastIceMeshRenderer.sharedMaterials;
                    currentCar.transform.localScale = new Vector3(1,1,1);
                    currentCar.transform.localPosition = new Vector3(0f, 0.486999989f, 0.273000002f);
                    currentCar.transform.localEulerAngles = new Vector3(0f, -90f, 0f);

                }
                else if (i == key)
                {
                    if (currentCarStr == "fast")
                    {
                        MeshFilter carMeshFilter = currentCar.GetComponent<MeshFilter>();
                        MeshRenderer carMeshRenderer = currentCar.GetComponent<MeshRenderer>();
                        carMeshFilter.mesh = fastDefaultMeshFilter.sharedMesh;
                        carMeshRenderer.materials = fastDefaultMeshRenderer.sharedMaterials;
                        currentCar.transform.localScale = new Vector3(28.04401f, 28.04401f, 28.04401f);
                        currentCar.transform.localPosition = new Vector3(-0.970000029f, 1.25600004f, 1.22800004f);
                        currentCar.transform.localEulerAngles = new Vector3(270f, 279.151886f, 0f);
                    }
                    else
                    {
                        currentCar.transform.localScale = new Vector3(1, 1, 1);
                        currentCar.transform.localPosition = new Vector3(-1.18834633e-16f, 1.21800005f, -0.177000001f);
                        currentCar.transform.localEulerAngles = new Vector3(270, 180, 0);
                    }
                    //add color unlocked?
                    if (tempColorBtn != null)
                    {
                        tempColorBtn.interactable = true;
                    }
                    carColourButtons[i].interactable = false;
                    tempColorBtn = carColourButtons[i];
                    carRenderer = currentCar.GetComponent<MeshRenderer>();
                    Material[] tempMats;
                    tempMats = carRenderer.materials;
                    tempMats[0] = carMaterials[i];

                    if (color == "gold" || color == "silver" || color == "bronze")
                    {
                        if (currentCarStr == "medium")
                        {
                            tempMats[5] = carMaterials[carMaterials.Length - 1];
                        }
                        else
                        {
                            tempMats[1] = carMaterials[carMaterials.Length - 1];
                        }
                    }
                    else
                    {
                        if (currentCarStr == "medium")
                        {
                            tempMats[5] = defaultWindowsMat;
                        }
                        else
                        {
                            tempMats[1] = defaultWindowsMat;
                        }
                    }
                    carRenderer.materials = tempMats;
                    newestMat = carMaterials[i];
                }
                
            }
            else
            {
                //carColourButtons[i].interactable = true;
            }
        }

        saveManager.SaveCar(currentCarStr, color, currentGunStr, currentPerkStr);
    }

    public void slowCarPressed()
    {
        slowCarBtn.interactable = false;
        mediumCarBtn.interactable = true;
        fastCarBtn.interactable = true;
        currentCar = slowCarBody;
        currentCarStr = "slow";
        saveManager.SaveCar("slow", currentColourStr, currentGunStr, currentPerkStr);
        carColourPressed(currentColourStr);
        resetCarVisuals();
    }

    public void mediumCarPressed()
    {
        if (mediumUnlocked)
        {
            slowCarBtn.interactable = true;
            mediumCarBtn.interactable = false;
            fastCarBtn.interactable = true;
            currentCar = mediumCarBody;
            currentCarStr = "medium";
            saveManager.SaveCar("medium", currentColourStr, currentGunStr, currentPerkStr);
            carColourPressed(currentColourStr);
            resetCarVisuals();
        }
        else
        {
            Debug.Log("buy??");

            if (theMoneyManager.checkIfEnough(mediumPrice))
            {
                saveManager.SaveMediumCarUnlock();
                mediumUnlock.SetActive(false);
                mediumUnlocked = true;
                slowCarBtn.interactable = true;
                mediumCarBtn.interactable = false;
                fastCarBtn.interactable = true;
                currentCar = mediumCarBody;
                currentCarStr = "medium";
                saveManager.SaveCar("medium", currentColourStr, currentGunStr, currentPerkStr);
                carColourPressed(currentColourStr);
                resetCarVisuals();
            }

            slowCarObj.SetActive(false);
            mediumCarObj.SetActive(true);
            fastCarObj.SetActive(false);
        }

        
    }

    public void fastCarPressed()
    {
        if (fastUnlocked)
        {
            slowCarBtn.interactable = true;
            mediumCarBtn.interactable = true;
            fastCarBtn.interactable = false;

            slowCarObj.SetActive(false);
            mediumCarObj.SetActive(false);
            fastCarObj.SetActive(true);
            currentCar = fastCarBody;
            currentCarStr = "fast";
            saveManager.SaveCar("fast", currentColourStr, currentGunStr, currentPerkStr);
            carColourPressed(currentColourStr);
            resetCarVisuals();
        }
        else
        {
            Debug.Log("buy??");

            if (theMoneyManager.checkIfEnough(mediumPrice))
            {
                saveManager.SaveFastCarUnlock();
                fastUnlock.SetActive(false);
                fastUnlocked = true;
                slowCarBtn.interactable = true;
                mediumCarBtn.interactable = true;
                fastCarBtn.interactable = false;
                currentCar = fastCarBody;
                currentCarStr = "fast";
                saveManager.SaveCar("fast", currentColourStr, currentGunStr, currentPerkStr);
                carColourPressed(currentColourStr);
                resetCarVisuals();
            }

            slowCarObj.SetActive(false);
            mediumCarObj.SetActive(false);
            fastCarObj.SetActive(true);
        }
        
    }

    public void pistolPressed()
    {
        pistolBtn.interactable = false;
        smgBtn.interactable = true;
        arBtn.interactable = true;

        currentGunStr = ("pistol");
        saveManager.SaveCar(currentCarStr, currentColourStr, "pistol", currentPerkStr);

        resetWeaponVisuals();

    }

    public void smgPressed()
    {

        if (smgUnlocked)
        {
            pistolBtn.interactable = true;
            smgBtn.interactable = false;
            arBtn.interactable = true;
            currentGunStr = ("smg");
            saveManager.SaveCar(currentCarStr, currentColourStr, "smg", currentPerkStr);
            resetWeaponVisuals();
        }
        else
        {
            Debug.Log("buy??");

            scPistol.SetActive(false);
            scSmg.SetActive(true);
            scRifle.SetActive(false);

            mcPistol.SetActive(false);
            mcSmg.SetActive(true);
            mcRifle.SetActive(false);

            fcPistol.SetActive(false);
            fcSmg.SetActive(true);
            fcRifle.SetActive(false);

            if (theMoneyManager.checkIfEnough(smgPrice))
            {
                saveManager.SaveSmgUnlock();
                smgUnlocked = true;
                smgUnlock.SetActive(false);
                pistolBtn.interactable = true;
                smgBtn.interactable = false;
                arBtn.interactable = true;
                currentGunStr = ("smg");
                saveManager.SaveCar(currentCarStr, currentColourStr, "smg", currentPerkStr);
                resetWeaponVisuals();
            }
        }
        
    }

    public void riflePressed()
    {
        if (rifleUnlocked)
        {
            pistolBtn.interactable = true;
            smgBtn.interactable = true;
            arBtn.interactable = false;
            currentGunStr = ("rifle");
            saveManager.SaveCar(currentCarStr, currentColourStr, "rifle", currentPerkStr);
            resetWeaponVisuals();
        }
        else
        {
            Debug.Log("buy??");

            scPistol.SetActive(false);
            scSmg.SetActive(false);
            scRifle.SetActive(true);

            mcPistol.SetActive(false);
            mcSmg.SetActive(false);
            mcRifle.SetActive(true);

            fcPistol.SetActive(false);
            fcSmg.SetActive(false);
            fcRifle.SetActive(true);

            if (theMoneyManager.checkIfEnough(riflePrice))
            {
                saveManager.SaveRifleUnlock();
                rifleUnlocked = true;
                rifleUnlock.SetActive(false);
                pistolBtn.interactable = true;
                smgBtn.interactable = true;
                arBtn.interactable = false;
                currentGunStr = ("rifle");
                saveManager.SaveCar(currentCarStr, currentColourStr, "rifle", currentPerkStr);
                resetWeaponVisuals();
            }
        }

    }

    public void maggedPerkPressed()
    {
        maggedBtn.interactable = false;
        if (intelectUnlocked)
        {
            intelectBtn.interactable = true;
        }
        if (richesUnlocked)
        {
            richesBtn.interactable = true;
        }
        currentPerkStr = "magged";
        saveManager.SaveCar(currentCarStr, currentColourStr, currentGunStr, "magged");
    }

    public void maggedUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(maggedPrice))
        {
            maggedPerkUnlock.SetActive(false);
            maggedBtn.interactable = true;
            maggedUnlocked = true;
        }
    }

    public void intelectPerkPressed()
    {
        intelectBtn.interactable = false;
        if (maggedUnlocked)
        {
            maggedBtn.interactable = true;
        }
        if (richesUnlocked)
        {
            richesBtn.interactable = true;
        }
        currentPerkStr = "intelect";
        saveManager.SaveCar(currentCarStr, currentColourStr, currentGunStr, "intelect");
    }

    public void intelectUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(intelectPrice))
        {
            intelectPerkUnlock.SetActive(false);
            intelectBtn.interactable = true;
            intelectUnlocked = true;
        }
    }

    public void richesPerkPressed()
    {
        if (maggedUnlocked)
        {
            maggedBtn.interactable = true;
        }
        if (intelectUnlocked)
        {
            intelectBtn.interactable = true;
        }
        richesBtn.interactable = false;
        currentPerkStr = "riches";
        saveManager.SaveCar(currentCarStr, currentColourStr, currentGunStr, "riches");
    }

    public void richesUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(richesPrice))
        {
            richesPerkUnlock.SetActive(false);
            richesBtn.interactable = true;
            richesUnlocked = true;
        }
    }

    public void mediumUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(mediumPrice))
        {
            mediumUnlock.SetActive(false);
            mediumCarBtn.interactable = true;
            mediumUnlocked = true;
        }
    }

    public void fastUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(fastPrice))
        {
            fastUnlock.SetActive(false);
            fastCarBtn.interactable = true;
            fastUnlocked = true;
        }
    }

    public void smgUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(smgPrice))
        {
            smgUnlock.SetActive(false);
            smgBtn.interactable = true;
            smgUnlocked = true;
        }
    }

    public void rifleUnlockPressed()
    {
        if (theMoneyManager.checkIfEnough(riflePrice))
        {
            rifleUnlock.SetActive(false);
            arBtn.interactable = true;
            rifleUnlocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.M)) 
        //{
        //    theMoneyManager.addToMoney(550);
        //}
    }
}
