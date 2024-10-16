using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour
{

    [SerializeField] private float bulletForce;

    private Rigidbody bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();
        DestroyObject(gameObject, 1f);
        bulletRb.AddForce(transform.right * bulletForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "target")
        {
            DestroyObject(gameObject);
        }
    }

}
