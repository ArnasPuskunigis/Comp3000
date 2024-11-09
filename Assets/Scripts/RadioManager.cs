using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioClip[] soundtracks;
    public int currentMusicIndex;
    public AudioSource currentTrack;

    // Start is called before the first frame update
    void Start()
    {
        if (soundtracks != null)
        {
            currentTrack = AudioManager.instance.GetPlaySfx(soundtracks[0], transform, 0.5f);
        }
    }

    public void playNextSong()
    {
        if (currentMusicIndex < soundtracks.Length - 1)
        {
            currentMusicIndex++;
            Destroy(currentTrack);
            //currentTrack = AudioManager.instance.GetPlaySfx(soundtracks[currentMusicIndex], transform, 0.5f);
        }
        else
        {
            currentMusicIndex = 0;
            Destroy(currentTrack);
            //currentTrack = AudioManager.instance.GetPlaySfx(soundtracks[currentMusicIndex], transform, 0.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            playNextSong();
        }
    }
}
