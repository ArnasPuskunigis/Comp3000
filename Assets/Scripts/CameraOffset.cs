using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public GameObject car;
    public Vector3 offset;
    public GameObject cameraRotBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(car.transform.position, Vector3.up, 20 * Time.deltaTime);

        Vector3 temp = new Vector3();
        temp = cameraRotBase.transform.rotation.eulerAngles;
        temp.y = car.transform.eulerAngles.y;
        cameraRotBase.transform.rotation = Quaternion.Euler(temp);
        //transform.position = car.transform.position + offset;
    }
}
