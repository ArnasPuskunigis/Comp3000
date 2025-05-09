using NWH.VehiclePhysics2.Demo;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{

    [SerializeField] private Collider[] trackColliders;

    [SerializeField] private Collider currentCheckpoint;
    [SerializeField] private int currentCheckpointInt;

    [SerializeField] private int currentLap = 0;

    [SerializeField] private GameObject greenCp;
    [SerializeField] private GameObject yellowCp;

    private GameObject tempGreenCp;
    private GameObject tempYellowCp;

    public Vector3 checkPointOffset;

    [SerializeField] private LapUI LapUIManager;
    [SerializeField] private Timer lapTimer;
    public exp expManager;
    public ResetTargets resetTargetsScript;


    // Start is called before the first frame update
    void Start()
    {
        resetTargetsScript = GameObject.Find("TargetsManager").GetComponent<ResetTargets>();
        expManager = GameObject.Find("ExpManager").GetComponent<exp>();
        lapTimer = GameObject.Find("TimerManager").GetComponent<Timer>();
        LapUIManager = GameObject.Find("LapUiManager").GetComponent<LapUI>();

        GameObject checkpoints = GameObject.Find("Checkpoints");
        int childCount = checkpoints.transform.childCount;
        trackColliders = new Collider[childCount];

        for (int i = 0; i <= childCount - 1; i++)
        {
            trackColliders[i] = checkpoints.transform.GetChild(i).GetComponent<Collider>();
        }

        resetTargetsScript = GameObject.Find("TargetsManager").GetComponent<ResetTargets>();
        currentCheckpoint = trackColliders[0];
        currentCheckpointInt = 0;
        currentLap = 1;
        LapUIManager.AddToLap(currentLap);
        tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
        tempYellowCp = Instantiate(yellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == trackColliders[trackColliders.Length - 1] && currentCheckpoint == trackColliders[trackColliders.Length - 1])
        {
            currentCheckpoint = trackColliders[0];
            currentLap += 1;
            currentCheckpointInt = 0;
            LapUIManager.AddToLap(currentLap);
            lapTimer.resetTimer();
            lapTimer.startTimer();
            expManager.lapCompleted();
            DestroyObject(tempGreenCp);
            DestroyObject(tempYellowCp);
            tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
            tempYellowCp = Instantiate(yellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);
            resetTargetsScript.resetTargets();
        }

        for (int i = 0; i < trackColliders.Length - 1; i++)
        {
            if (other == trackColliders[i] && other != trackColliders[trackColliders.Length - 2] && currentCheckpoint == trackColliders[i])
            {
                currentCheckpointInt++;
                DestroyObject(tempGreenCp);
                DestroyObject(tempYellowCp);
                tempGreenCp = Instantiate(greenCp, trackColliders[i + 1].transform.position - checkPointOffset, trackColliders[i + 1].transform.rotation);
                tempYellowCp = Instantiate(yellowCp, trackColliders[i + 2].transform.position - checkPointOffset, trackColliders[i + 2].transform.rotation);
                currentCheckpoint = trackColliders[i + 1];
            }
            else if (other == trackColliders[trackColliders.Length - 2] && currentCheckpoint == trackColliders[trackColliders.Length - 2])
            {
                DestroyObject(tempGreenCp);
                DestroyObject(tempYellowCp);
                tempGreenCp = Instantiate(greenCp, trackColliders[trackColliders.Length - 1].transform.position - checkPointOffset, trackColliders[trackColliders.Length - 1].transform.rotation);
                tempYellowCp = Instantiate(yellowCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
                currentCheckpoint = trackColliders[trackColliders.Length - 1];
            }
            else if (other == trackColliders[trackColliders.Length - 1] && currentCheckpoint == trackColliders[trackColliders.Length - 1])
            {
                DestroyObject(tempGreenCp);
                DestroyObject(tempYellowCp);
                tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
                tempYellowCp = Instantiate(yellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);
                currentCheckpoint = trackColliders[0];
            }
        }

    }
    void FixedUpdate()
    {
        
    }
}
