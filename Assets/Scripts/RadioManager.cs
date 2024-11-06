using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioClip[] soundtracks; 

    // Start is called before the first frame update
    void Start()
    {
        if (soundtracks != null)
        {
            AudioManager.instance.PlaySfx(soundtracks[0], transform, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
