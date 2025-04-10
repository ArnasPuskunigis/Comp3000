using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFloatAndFadeAway : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textObj;

    public float elapsedTime;
    public float inTime;
    public float outTime;

    public bool animateTarget;
    public bool animateTargetOff;

    void Start()
    {
        animateTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (animateTarget)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / inTime;
            textObj.color = new Color(255, 255, 255, Mathf.Lerp(0, 1, percentageComplete));
        }

        if (textObj.color.a == 1)
        {
            animateTarget = false;
            animateTargetOff = true;
            elapsedTime = 0;
        }

        if (animateTargetOff)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / outTime;
            textObj.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, percentageComplete));
            textObj.rectTransform.anchoredPosition = new Vector3(0, Mathf.Lerp(150, 300, percentageComplete), 0);
        }

        if (textObj.color.a == 0)
        {
            Destroy(gameObject);
        }
    }
}
