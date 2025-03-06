using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceCar : MonoBehaviour
{
    public float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == ("car"))
        {
            Debug.Log(collision.collider);
            collision.collider.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.GetComponent<Rigidbody>().velocity * bulletForce, collision.GetContact(0).point, ForceMode.Impulse);
        }
    }

}
