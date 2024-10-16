using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaser : MonoBehaviour
{

    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Vector3[] allVectors;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private GameObject laserHitObject;
    [SerializeField] private GameObject laserHitsParent;

    public Vector3 yes;

    public int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        allVectors = new Vector3[2];
        layerMask = ~layerMask;

    }

    // Update is called once per frame
    void Update()
    {
        allVectors[0] = transform.TransformPoint(new Vector3(0, 0, 0));
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            allVectors[1] = hit.point;
            Instantiate(laserHitObject, hit.point, Quaternion.EulerRotation(hit.normal + yes), laserHitsParent.transform);
        }
        else
        {
            allVectors[1] = transform.position + transform.forward * 100;
        }
        Debug.DrawRay(transform.position, transform.forward);
        laserLine.SetPositions(allVectors);
    }
}
