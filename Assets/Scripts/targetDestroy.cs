using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class targetDestroy : MonoBehaviour
{

    [SerializeField] GameObject destroyedTarget;
    [SerializeField] bool basic;
    [SerializeField] GameObject targetsParent;

    [SerializeField] ScoreManager theScoreManager;
    [SerializeField] exp expManager;
    public multiplierManager multManager;

    // Start is called before the first frame update
    void Start()
    {
        targetsParent = GameObject.Find("destroyed");
        theScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        expManager = GameObject.Find("ExpManager").GetComponent<exp>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "bullet" || collision.transform.tag == "car")
        {
            Instantiate(destroyedTarget, transform.position, transform.rotation, targetsParent.transform);
            if (basic)
            {
                //theScoreManager.addScore(1);
                expManager.targetHit(1);
                multManager.getTargets(1);
            }
            else
            {
                //theScoreManager.addScore(3);
                expManager.targetHit(25);
                multManager.getTargets(25);
            }
            gameObject.SetActive(false);
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
