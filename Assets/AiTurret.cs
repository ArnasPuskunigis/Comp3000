using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiTurret : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] float distanceLimit;
    [SerializeField] float distanceBetween;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
        {
            lookForTarget();
        }
        else
        {
            followTarget();
        }
    }

    private void lookForTarget()
    {
        GameObject foundObject = GameObject.FindGameObjectWithTag("car");
        if (foundObject != null)
        {
            targetObject = foundObject;
        }
    }

    private void followTarget()
    {
        distanceBetween = Vector3.Distance(transform.position, targetObject.transform.position);
        if (distanceBetween < distanceLimit)
        {
            navAgent.SetDestination(targetObject.transform.position);
        }
    }

}
