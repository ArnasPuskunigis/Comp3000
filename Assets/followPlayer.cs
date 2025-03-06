using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{

    public GameObject player;
    public Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("car");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        rb.AddForce(transform.forward * speed);
    }
}
