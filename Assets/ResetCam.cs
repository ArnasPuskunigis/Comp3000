using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetCam : MonoBehaviour
{
    public GameObject cam;
    public GameObject scar;
    public GameObject mcar;
    public GameObject fcar;

    public SavingSystem saveManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.Three))
        {

            if (saveManager.carTypeStr == "slow")
            {
                cam.transform.LookAt(scar.transform);
            }
            if (saveManager.carTypeStr == "medium")
            {
                cam.transform.LookAt(mcar.transform);
            }
            if (saveManager.carTypeStr == "fast")
            {
                cam.transform.LookAt(fcar.transform);
            }
        }
    }
}
