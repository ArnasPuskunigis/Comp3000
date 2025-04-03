using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFlip : MonoBehaviour
{

    public GameObject theCar;
    public loadCar carLoader;
    public bool carFound;

    // Start is called before the first frame update
    void Start()
    {
        carLoader = GameObject.Find("CarLoader").GetComponent<loadCar>();
        if (carLoader.currentCar != null)
        {
            theCar = carLoader.currentCar;
            carFound = true;
        }
        else
        {
            carFound = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carFound)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                theCar.transform.rotation = Quaternion.Euler(theCar.transform.rotation.x, theCar.transform.rotation.y, 0);
                ExplodeCar.instance.DestroyCar();
            }
        }
        else if (!carFound && carLoader.currentCar != null)
        {
            theCar = carLoader.currentCar;
            carFound = true;
        }
    }
}
