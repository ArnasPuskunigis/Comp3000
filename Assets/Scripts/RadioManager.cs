using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioClip[] soundtracks;
    public AudioSource[] soundPlayers;
    public int currentMusicIndex;
    public float musicVolume;

    public static RadioManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
   {
        if (Instance == null)
        {
            Instance = this;
            // Make sure this instance persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate instance if it already exists
            Destroy(gameObject);
        }

        if (soundtracks != null && soundtracks.Length > 0)
        {
            // Initialize the soundPlayers array
            soundPlayers = new AudioSource[soundtracks.Length];

            for (int i = 0; i < soundtracks.Length; i++)
            {
                // Create a new AudioSource for each soundtrack
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = soundtracks[i];
                soundPlayers[i] = audioSource;
                soundPlayers[i].volume = musicVolume;
                soundPlayers[i].Play();
                soundPlayers[i].Pause();
            }

            // Start playing the first track
            currentMusicIndex = 0;
            soundPlayers[currentMusicIndex].UnPause();
        }
   }

    public void playNextSong()
    {
        soundPlayers[currentMusicIndex].Pause();

        if (currentMusicIndex < soundtracks.Length - 1)
        {
            currentMusicIndex++;
        }
        else
        {
            currentMusicIndex = 0;
        }
        
        soundPlayers[currentMusicIndex].UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || !soundPlayers[currentMusicIndex].isPlaying)
        {
            playNextSong();
        }


    }
}
