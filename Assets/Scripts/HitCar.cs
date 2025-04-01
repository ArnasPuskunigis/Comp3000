using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCar : MonoBehaviour
{
    public float bulletForce;
    public int bulletDamage = 8;
    public CarHealth carHp;

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
            collision.collider.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.GetComponent<Rigidbody>().velocity * bulletForce, collision.GetContact(0).point, ForceMode.Impulse);
            carHp = collision.transform.GetComponent<CarHealth>();
        
            if (carHp != null && !carHp.isDead) 
            {
                carHp.DealDamage(bulletDamage);
            }
                
        }
    }

}
