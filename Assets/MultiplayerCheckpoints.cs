using TMPro;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerCheckpoints : NetworkBehaviour
{

    public NetworkVariable<float> distanceToCP = new NetworkVariable<float>(
        writePerm: NetworkVariableWritePermission.Owner,
        readPerm: NetworkVariableReadPermission.Everyone
    );

    public NetworkVariable<int> currentCP = new NetworkVariable<int>(
        writePerm: NetworkVariableWritePermission.Owner,
        readPerm: NetworkVariableReadPermission.Everyone
    );

    public NetworkVariable<int> racePosition = new NetworkVariable<int>(
        writePerm: NetworkVariableWritePermission.Owner,
        readPerm: NetworkVariableReadPermission.Everyone
    );

    public NetworkVariable<int> currentLap = new NetworkVariable<int>(
        writePerm: NetworkVariableWritePermission.Owner,
        readPerm: NetworkVariableReadPermission.Everyone
    );



    [SerializeField] private Collider[] trackColliders;

    [SerializeField] private Collider currentCheckpoint;
    [SerializeField] private int currentCheckpointInt;

    //[SerializeField] private int currentLap = 0;

    [SerializeField] private LapUI LapUIManager;
    //[SerializeField] private Timer lapTimer;

    [SerializeField] private GameObject greenCp;
    [SerializeField] private GameObject yellowCp;

    private GameObject tempGreenCp;
    private GameObject tempYellowCp;

    public Vector3 checkPointOffset;

    //public exp expManager;

    //public ResetTargets resetTargetsScript;

    public TextMeshProUGUI racePosText;
    public RaceManager raceManager;

    // RPC to update the position on all clients
    [ClientRpc]
    public void UpdateRacePositionOnClientsClientRpc(int newPosition, int playerCount)
    {
        if (IsOwner)
        {
            // Update the UI to reflect the new position
            //racePosText.text = $"Position: {newPosition}";
            racePosText.text = $"POS : {newPosition}" + "/" + playerCount;
        }
    }

    void Start()
    {
        if (!IsOwner) return;

        //raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        //raceManager.AddPlayer(this);
        LapUIManager = GameObject.Find("LapUiManager").GetComponent<LapUI>();
        racePosText = GameObject.Find("PositionText").GetComponent<TextMeshProUGUI>();
        GameObject checkpoints = GameObject.Find("Checkpoints");
        int childCount = checkpoints.transform.childCount;
        trackColliders = new Collider[childCount];

        for (int i = 0; i <= 16; i++)
        {
            trackColliders[i] = checkpoints.transform.GetChild(i).GetComponent<Collider>();
        }

        //resetTargetsScript = GameObject.Find("TargetsManager").GetComponent<ResetTargets>();
        currentCheckpoint = trackColliders[0];
        currentCheckpointInt = 0;
        currentLap.Value = 1;
        LapUIManager.AddToLap(currentLap.Value);
        //tempGreenCp = Instantiate(greenCp, trackColliders[trackColliders.Length - 1].transform.position - checkPointOffset, trackColliders[trackColliders.Length - 1].transform.rotation);
        tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
        tempYellowCp = Instantiate(yellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;

        if (other == trackColliders[trackColliders.Length - 1] && currentCheckpoint == trackColliders[trackColliders.Length - 1])
        {
            currentCheckpoint = trackColliders[0];
            currentLap.Value += 1;
            currentCheckpointInt = 0;
            LapUIManager.AddToLap(currentLap.Value);
            //lapTimer.resetTimer();
            //lapTimer.startTimer();
            //expManager.lapCompleted();
            DestroyObject(tempGreenCp);
            DestroyObject(tempYellowCp);
            tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
            tempYellowCp = Instantiate(yellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);
            //resetTargetsScript.resetTargets();
        }

        for (int i = 0; i < trackColliders.Length - 1; i++)
        {
            if (other == trackColliders[i] && other != trackColliders[trackColliders.Length - 2] && currentCheckpoint == trackColliders[i])
            {
                currentCheckpointInt++;
                DestroyObject(tempGreenCp);
                DestroyObject(tempYellowCp);
                tempGreenCp = Instantiate(greenCp, trackColliders[i + 1].transform.position- checkPointOffset, trackColliders[i + 1].transform.rotation);
                tempYellowCp = Instantiate(yellowCp, trackColliders[i + 2].transform.position- checkPointOffset, trackColliders[i + 2].transform.rotation);
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
        if (!IsOwner) return;
        distanceToCP.Value = Vector3.Distance(trackColliders[currentCheckpointInt].transform.position, transform.position);
        currentCP.Value = currentCheckpointInt;
    }
}
