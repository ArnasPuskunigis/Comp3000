using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseEditor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!UnityEditor.EditorApplication.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Break();
            }
        }
    }
}
