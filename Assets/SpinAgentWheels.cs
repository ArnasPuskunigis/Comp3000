using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpinAgentWheels : MonoBehaviour
{
    [SerializeField] Transform[] wheels;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] float agentSpeed;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agentSpeed = navAgent.velocity.magnitude;
        if (navAgent != null)
        {
            if (agentSpeed > 1f)
            {
                foreach (Transform wheel in wheels)
                {
                    wheel.rotation = Quaternion.Euler(wheel.rotation.eulerAngles.x, wheel.rotation.eulerAngles.y, wheel.rotation.eulerAngles.z - (rotationSpeed * agentSpeed));
                }
            }
        }
    }
}
