using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudioScript : MonoBehaviour
{

    public float audioPitch = 1f;
    public AudioSource engineAudioSource;
    public Rigidbody carRb;
    public AudioSource engineStartSource;
    public bool engineStarted;
    public carType car;
    public float engineSoundMult;
    public AudioSource driftAudioSource;

    public bool drifting;
    public enum carType
    {
        Slow,
        Medium,
        Fast
    };

    // Start is called before the first frame update
    void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
        if (car == carType.Slow)
        {
            Invoke("enableEngine", 2f);
        }
        else if (car == carType.Medium)
        {
            Invoke("enableEngine", 3f);
        }
        else if (car == carType.Fast)
        {
            Invoke("enableEngine", 2f);
        }
    }

    public void enableEngine()
    {
        engineStarted = true;
        engineAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (engineStarted)
        {
            audioPitch = (carRb.velocity.magnitude / 10) * engineSoundMult;
            engineAudioSource.pitch = audioPitch;
        }

        if (drifting)
        {
            driftAudioSource.volume = 0.3f;
        }
        else
        {
            driftAudioSource.volume = 0f;
        }

    }
}
