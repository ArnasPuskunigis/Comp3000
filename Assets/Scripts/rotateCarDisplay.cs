using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rotateCarDisplay : MonoBehaviour
{

    public Camera camObject;
    public bool isRotating;
    public GameObject rotationObject;
    
    public float rotationSpeed;

    public bool canIgnore;

    public Vector2 oldMousePos = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        //camObject = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camObject.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.tag.Equals("car") || canIgnore)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (isRotating)
                    {
                        if (oldMousePos.x < Input.mousePosition.x)
                        {
                            rotationObject.transform.Rotate(0, 0, -rotationSpeed);
                        }
                        else if (oldMousePos.x > Input.mousePosition.x)
                        {
                            rotationObject.transform.Rotate(0, 0, rotationSpeed);
                        }
                        canIgnore = true;
                    }
                    else
                    {
                        isRotating = true;
                    }

                    oldMousePos = Input.mousePosition;

                }

                else
                {
                    canIgnore = false;
                    isRotating = false;
                    oldMousePos = new Vector2(0, 0);
                }
            }
        }


    }



}
