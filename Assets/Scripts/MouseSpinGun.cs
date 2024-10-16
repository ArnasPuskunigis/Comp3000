using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpinGun : MonoBehaviour
{
    public Camera camObject;

    public Transform gunRot;

    private void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camObject.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {

            gunRot.transform.LookAt(hit.point);
        }

    }
}
