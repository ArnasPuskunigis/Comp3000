using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class boxExplode : MonoBehaviour
{
    public GameObject explodedCrate;
    public GameObject explosionParent;
    public GameObject explodedCrateTemp;

    public bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explodeBox()
    {
        if (explodedCrateTemp == null)
        {
            explodedCrateTemp = Instantiate(explodedCrate, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3 (91, 8, -95);
            exploded = true;
        }
    }

    public void resetBox()
    {
        Destroy(explodedCrateTemp);
        gameObject.SetActive(true);
        exploded = false;
    }


        private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("smash"))
        {
            explodedCrateTemp = Instantiate(explodedCrate, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(91, 8, -95);
        }
    }
}
