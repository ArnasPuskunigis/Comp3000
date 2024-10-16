using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private int Score = 0;
    [SerializeField] private TextMeshProUGUI ScoreText;

    public void addScore(int score)
    {
        Score += score;
        ScoreText.text = "Score: " + Score;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: " + Score;
    }

}
