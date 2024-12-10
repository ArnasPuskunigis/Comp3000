using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpinGun : MonoBehaviour
{

    public Transform gunRot;
    public LayerMask IgnoreMe;


    [Range(0.1f, 5f)]
    public float rotationSpeed;

    public bool mouseControls;
    public Vector2 mousePos;

    public bool EnableVR;
    public GameObject rightHand;

    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) & !mouseControls)
        {
            Vector3 newRot = new Vector3();
            newRot.x = gunRot.rotation.eulerAngles.x;
            newRot.y = gunRot.rotation.eulerAngles.y;
            newRot.z = gunRot.rotation.eulerAngles.z - rotationSpeed;
            gunRot.eulerAngles = newRot;
        }
        if (Input.GetKey(KeyCode.X) & !mouseControls)
        {
            Vector3 newRot = new Vector3();
            newRot.x = gunRot.rotation.eulerAngles.x;
            newRot.y = gunRot.rotation.eulerAngles.y;
            newRot.z = gunRot.rotation.eulerAngles.z + rotationSpeed;
            gunRot.eulerAngles = newRot;
        }

        if (mouseControls)
        {
            if (EnableVR) 
            {
                Ray ray = new Ray(rightHand.transform.position, rightHand.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10000f, ~IgnoreMe))
                {
                    gunRot.transform.LookAt(hit.point);
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10000f, ~IgnoreMe))
                {
                    gunRot.transform.LookAt(hit.point);
                }
            }

        }

    }
}
