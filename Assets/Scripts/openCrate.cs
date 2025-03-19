using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class openCrate : MonoBehaviour
{
    public int crateCount;

    public float randomNumber;

    public float lower = 1;
    public float upper = 100;

    [Header("Basic crate")]
    [Range(1.0f, 100.0f)]
    public float commonChance;
    [Range(1.0f, 100.0f)]
    public float rareChance;
    [Range(1.0f, 100.0f)]
    public float epicChance;
    [Range(1.0f, 100.0f)]
    public float legendaryChance;

    [Header("Rare crate")]
    [Range(1.0f, 100.0f)]
    public float r_commonChance;
    [Range(1.0f, 100.0f)]
    public float r_rareChance;
    [Range(1.0f, 100.0f)]
    public float r_epicChance;
    [Range(1.0f, 100.0f)]
    public float r_legendaryChance;

    public TMPro.TextMeshProUGUI crateCountText;

    public int commonCount;
    public int rareCount;
    public int epicCount;
    public int legendaryCount;

    public GameObject commonImage;
    public GameObject rareImage;
    public GameObject epicImage;
    public GameObject legendaryImage;

    public boxExplode boxAnimManager;
    public SavingSystem saveManager;
    public mainUiManager uiManager;

    public string recentRarirty;
    public bool isItem;
    public string recentItem;

    public bool itemReceived;

    public moneyManager moneyManager;

    public TMPro.TextMeshProUGUI moneyRewardText;

    public int tempMoneyWon;

    public bool EnableVR;

    // Start is called before the first frame update
    void Start()
    {
        crateCount = PlayerPrefs.GetInt("CrateCount");
        crateCountText.text = "Crates Remaining: " + crateCount;
    }

    public void showRarity(Animator rarityImage)
    {
        rarityImage.SetBool("fadeIn", true);
        itemReceived = false;
        Invoke("showItem", 1);
    }

    public void showItem()
    {
        if (recentRarirty == "common")
        {
            tempMoneyWon = Random.Range(25,101);
            moneyRewardText.text = "$" + tempMoneyWon + "!";
        }
        else if (recentRarirty == "rare")
        {
            if (recentItem != null)
            {
                uiManager.checkForUnlocks();
                if (recentItem == "cyan" && !checkIfColorOwned("cyan"))
                {
                    moneyRewardText.text = "VERY RARE ITEM RECEIVED! \n CYAN CAR COLOR!";
                    saveManager.SaveCyanUnlock();
                }
                else if (recentItem == "lavender" && !checkIfColorOwned("lavender"))
                {
                    moneyRewardText.text = "VERY RARE ITEM RECEIVED! \n LAVENDER CAR COLOR!";
                    saveManager.SaveLavenderUnlock();
                }
                else if (recentItem == "peach" && !checkIfColorOwned("peach"))
                {
                    saveManager.SavePeachUnlock();
                    moneyRewardText.text = "VERY RARE ITEM RECEIVED! \n PEACH CAR COLOR!";
                }
                else if (recentItem == "pink" && !checkIfColorOwned("pink"))
                {
                    saveManager.SavePinkUnlock();
                    moneyRewardText.text = "VERY RARE ITEM RECEIVED! \n PINK CAR COLOR!";
                }
                else
                {
                    tempMoneyWon = Random.Range(150, 301);
                    moneyRewardText.text = "$" + tempMoneyWon + "!";
                }
                uiManager.checkForUnlocks();
                recentItem = null;
            }
            else
            {
                tempMoneyWon = Random.Range(150, 301);
                moneyRewardText.text = "$" + tempMoneyWon + "!";
            }
        }
        else if (recentRarirty == "epic")
        {
            tempMoneyWon = Random.Range(400, 701);
            moneyRewardText.text = "$" + tempMoneyWon + "!";
        }
        else if (recentRarirty == "legendary")
        {
            tempMoneyWon = Random.Range(1000, 1501);
            moneyRewardText.text = "$" + tempMoneyWon + "!";
        }
        Invoke("showRewardText", 1f);
        itemReceived = true;
    }

    public bool checkIfColorOwned(string colorName)
    {
        if (colorName == "cyan")
        {
            if (saveManager.LoadCyanUnlock())
            {
                return true;
            }
        }
        else if (colorName == "lavender")
        {
            if (saveManager.LoadLavenderUnlock())
            {
                return true;
            }
        }
        else if(colorName == "peach")
        {
            if (saveManager.LoadPeachUnlock())
            {
                return true;
            }
        }
        else if(colorName == "pink")
        {
            if (saveManager.LoadPinkUnlock())
            {
                return true;
            }
        }
        return false;
    }

    public void showRewardText()
    {
        moneyRewardText.gameObject.SetActive(true);
        moneyManager.addToMoney(tempMoneyWon);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            crateCount += 5;
            crateCountText.text = "Crates Remaining: " + crateCount;
        }

        if (EnableVR)
        {
            if (OVRInput.Get(OVRInput.Button.Four) && crateCount >= 1 && itemReceived)
            //if (crateCount >= 1)
            {
                if (boxAnimManager.exploded)
                {
                    resetBoxUI();
                }
                else
                {
                    openTheCrate();
                }

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && crateCount >= 1 && itemReceived)
            //if (crateCount >= 1)
            {
                if (boxAnimManager.exploded)
                {
                    resetBoxUI();
                }
                else
                {
                    openTheCrate();
                }

            }
        }

    }

    public void resetBoxUI()
    {
        if (boxAnimManager.exploded)
        {
            boxAnimManager.resetBox();
            moneyRewardText.gameObject.SetActive(false);
            moneyRewardText.text = "";
            commonImage.GetComponent<Animator>().SetBool("fadeIn", false);
            rareImage.GetComponent<Animator>().SetBool("fadeIn", false);
            epicImage.GetComponent<Animator>().SetBool("fadeIn", false);
            legendaryImage.GetComponent<Animator>().SetBool("fadeIn", false);
            commonImage.GetComponent<Animator>().StopPlayback();
            rareImage.GetComponent<Animator>().StopPlayback();
            epicImage.GetComponent<Animator>().StopPlayback();
            legendaryImage.GetComponent<Animator>().StopPlayback();
        }
    }
    public void openTheCrate()
    {
        crateCount--;
        crateCountText.text = "Crates Remaining: " + crateCount;
        randomNumber = Random.Range(lower, upper);
        boxAnimManager.explodeBox();
        boxAnimManager.exploded = true;


        int rng = Random.Range(1, 4);
        if (rng == 1)
        {
            isItem = true;
        }
        else
        {
            isItem = false;
        }

        if (randomNumber <= commonChance)
        {
            print("common");
            recentRarirty = "common";

            commonImage.SetActive(true);
            showRarity(commonImage.GetComponent<Animator>());
            commonCount++;
        }
        else if (randomNumber > commonChance && randomNumber <= 100 - epicChance)
        {
            if (isItem)
            {
                chooseItem();
            }
            else
            {
                recentItem = null;
            }
            print("rare");
            recentRarirty = "rare";
            rareImage.SetActive(true);
            showRarity(rareImage.GetComponent<Animator>());
            rareCount++;
        }
        else if (randomNumber > commonChance + rareChance && randomNumber <= 100 - legendaryChance)
        {
            print("epic");
            recentRarirty = "epic";
            epicImage.SetActive(true);
            showRarity(epicImage.GetComponent<Animator>());
            epicCount++;
        }
        else if (randomNumber > commonChance + rareChance + epicChance && randomNumber <= 100)
        {
            print("legendary");
            recentRarirty = "legendary";
            legendaryImage.SetActive(true);
            showRarity(legendaryImage.GetComponent<Animator>());
            legendaryCount++;
        }

        PlayerPrefs.SetInt("CrateCount", crateCount);
        PlayerPrefs.Save();
    }

    public void chooseItem()
    {
        int randomNumber = Random.Range(0, 4);

        if (randomNumber == 0)
        {
            recentItem = "cyan";
        }
        else if (randomNumber == 1)
        {
            recentItem = "lavender";
        }
        else if (randomNumber == 2)
        {
            recentItem = "peach";
        }
        else
        {
            recentItem = "pink";
        }
    }

}