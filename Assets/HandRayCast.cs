using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRayCast : MonoBehaviour
{
    public float rayLength = 10f; // Length of the ray
    public LayerMask hitLayers;  // Layers to include in the raycast

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Define the ray's origin and direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Draw the ray in the Scene view for visualization
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.blue);

        // Perform the raycast
        //if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, rayLength, hitLayers))
        //{
        //    Debug.Log("Hit: " + hitInfo.collider.name);
        //}
    }
}
