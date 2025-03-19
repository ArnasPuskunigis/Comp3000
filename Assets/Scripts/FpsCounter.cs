using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FpsCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fpsText;
    public int fpsLimit;
    private float[] fpsCollection = new float[59];
    private int frameIndex;

    // Start is called before the first frame update
    void Start()
    {
        fpsText = GameObject.Find("FpsCounter").GetComponent<TextMeshProUGUI>();
        frameIndex = 0;
        Application.targetFrameRate = fpsLimit;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCollection[frameIndex] = 1f / Time.deltaTime;
        frameIndex++;

        if (frameIndex >= fpsCollection.Length)
        {
            frameIndex = 0;
        }

        fpsText.text = "FPS: " + Mathf.RoundToInt(calculateFPS()).ToString(); 
    }

    public float calculateFPS()
    {
        float total = 0f;

        foreach (float fps in fpsCollection)
        {
            total += fps;
        }

        return total/fpsCollection.Length;
    }

}
