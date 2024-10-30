using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ResetTargets : MonoBehaviour
{
    public GameObject commonTargetsParent;

    public List<GameObject> allTargets;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform target in commonTargetsParent.transform) 
        {
            allTargets.Add(target.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T)) 
        {
            resetTargets();
        }
    }

    public void resetTargets()
    {
        foreach (GameObject target in allTargets)
        {
            target.SetActive(true);
        }
    }

}
