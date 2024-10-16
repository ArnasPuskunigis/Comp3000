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

    public string recentRarirty;
    public bool itemReceived;

    // Start is called before the first frame update
    void Start()
    {
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
            //add money 50
            //add matte skins for s-31
            //add matte skins for pistol
        }
        else if (recentRarirty == "rare")
        {
            //add money 100
            //add gold skin for m-71
            //add gold skin for smg
        }
        else if (recentRarirty == "epic")
        {
            //add perks 1,2, or 3
            //add xp tokens??
        }
        else if (recentRarirty == "legendary")
        {
            //add ice skin for f-71
            //add ice skin for smg
        }
        Debug.Log("item");
        itemReceived = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            crateCount+=5;
            crateCountText.text = "Crates Remaining: " + crateCount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && crateCount >=1 && itemReceived)
        //if (crateCount >= 1)
        {
            if (boxAnimManager.exploded)
            {
                boxAnimManager.resetBox();
                commonImage.GetComponent<Animator>().SetBool("fadeIn", false);
                rareImage.GetComponent<Animator>().SetBool("fadeIn", false);
                epicImage.GetComponent<Animator>().SetBool("fadeIn", false);
                legendaryImage.GetComponent<Animator>().SetBool("fadeIn", false);
            }
            else
            {
                openTheCrate();
                
            }


        }

    }

    public void openTheCrate()
    {
        crateCount--;
        crateCountText.text = "Crates Remaining: " + crateCount;
        randomNumber = Random.Range(lower, upper);
        print(randomNumber);
        boxAnimManager.explodeBox();
        boxAnimManager.exploded = true;

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
    }

    public void chooseItem()
    {
        int randomNumber = Random.Range(0, 4);


        if (randomNumber == 0)
        {

        }
        else if (randomNumber == 1)
        {

        }
        else if (randomNumber == 2)
        {

        }
        else if (randomNumber == 3)
        {

        }

    }

}