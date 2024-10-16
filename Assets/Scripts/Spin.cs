using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Rigidbody SpinRb;
    public int SpinStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpinRb.AddForce(SpinRb.gameObject.transform.forward* SpinStrength);
    }
}
