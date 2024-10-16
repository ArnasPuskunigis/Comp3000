using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class textFadeFloat : MonoBehaviour
{
    public exp expManager;

    public TMPro.TextMeshProUGUI textObj;

    public float elapsedTime;
    public float inTime;
    public float outTime;

    public bool animateTarget;
    public bool animateTargetOff;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        expManager = GameObject.Find("ExpManager").GetComponent<exp>();
        animateTarget = true;
        textObj.rectTransform.anchoredPosition = new Vector3(291, 48, 0);
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("uiparent").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (animateTarget)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / inTime;
                if (type == "yellow")
                {
                    textObj.text = "+" + 25 * expManager.expMultiplier + "XP ULTRA HIT";
                    textObj.color = new Color(255, 230, 0, Mathf.Lerp(0, 1, percentageComplete));
                }
                else if (type == "lap")
                {
                    textObj.text = "+" + 100 * expManager.expMultiplier + "XP LAP COMPLETE";
                    textObj.color = new Color(0, 255, 255, Mathf.Lerp(0, 1, percentageComplete));
                }
                else if (type == "hit")
                {
                    textObj.text = "+" + 1 * expManager.expMultiplier + "XP HIT";
                    textObj.color = new Color(255, 255, 255, Mathf.Lerp(0, 1, percentageComplete));
                }
                else if (type == "drift")
                {
                    textObj.text = "+" + expManager.driftExpGain + "XP DRIFT";
                    textObj.color = new Color(255, 255, 255, Mathf.Lerp(0, 1, percentageComplete));
                }
                textObj.rectTransform.anchoredPosition = new Vector3(291, 48, 0);
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
                if (type == "yellow")
                {
                    textObj.color = new Color(255, 230, 0, Mathf.Lerp(1, 0, percentageComplete));
                }
                else if (type == "lap")
                {
                    textObj.color = new Color(0, 255, 255, Mathf.Lerp(1, 0, percentageComplete));
                }
                else if (type == "hit")
                {
                    textObj.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, percentageComplete));

                }
                else if (type == "drift")
                {
                    textObj.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, percentageComplete));

                }
                textObj.rectTransform.anchoredPosition = new Vector3(291, Mathf.Lerp(48, 70, percentageComplete), 0);
            }

            if (textObj.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
