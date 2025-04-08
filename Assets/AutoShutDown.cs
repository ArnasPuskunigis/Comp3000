using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AutoShutDown : MonoBehaviour
{
    private float timer;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        timer = 600;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timer)
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.Shutdown();
        }
    }

}
