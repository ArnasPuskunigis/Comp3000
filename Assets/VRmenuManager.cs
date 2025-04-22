using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRmenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SetSelectrable(GameObject selectableObj)
    {
        selectableObj.GetComponent<Selectable>().Select();
    }

}
