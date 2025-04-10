using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

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

    public GameObject whoFinishedRaceText;
    public TextMeshProUGUI racePosText;
    public GameObject UICanvas;
    //public RaceManager raceManager;

    // RPC to update the position on all clients
    [ClientRpc]
    public void UpdateRacePositionOnClientsClientRpc(int newPosition, int playerCount)
    {
        if (IsOwner)
        {
            racePosText.text = $"POS : {newPosition}" + "/" + playerCount;
        }
    }

    [ClientRpc]
    public void ShowWhoFinishedTheRaceClientRpc(string playerName, int playerPosition)
    {
        if (IsOwner)
        {
            GameObject finishedtext = Instantiate(whoFinishedRaceText, UICanvas.transform);
            string suffix = GetSuffix(playerPosition);
            finishedtext.GetComponent<TextMeshProUGUI>().text = playerName + " FINISHED IN " + playerPosition + suffix + "!";
        }
    }

  

    string GetSuffix(int number)
    {
        int lastTwoDigits = number % 100;
        int lastDigit = number % 10;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
        {
            return "TH";
        }

        return lastDigit switch
        {
            1 => "ST",
            2 => "ND",
            3 => "RD",
            _ => "TH",
        };
    }

    void Start()
    {
        if (!IsOwner) return;

        UICanvas = GameObject.Find("==== UI ====");
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
