using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoints : NetworkBehaviour
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

    [SerializeField] private Collider[] trackColliders;

    [SerializeField] private Collider currentCheckpoint;
    [SerializeField] private int currentCheckpointInt;

    [SerializeField] private int currentLap = 0;

    //[SerializeField] private LapUI LapUIManager;
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

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            distanceToCP.OnValueChanged += OnDistanceChanged;
            currentCP.OnValueChanged += OnCPChanged;
        }
    }

    // RPC to inform all clients about the updated position
    [ServerRpc(RequireOwnership = false)]
    public void UpdateRacePositionServerRpc(int newPosition)
    {
        racePosition.Value = newPosition;
        UpdateRacePositionOnClientsClientRpc(newPosition);
    }

    // RPC to update the position on all clients
    [ClientRpc]
    public void UpdateRacePositionOnClientsClientRpc(int newPosition)
    {
        if (IsOwner)
        {
            // Update the UI to reflect the new position
            //racePosText.text = $"Position: {newPosition}";
            racePosText.text = $"POS : {newPosition}" + "/2";
        }
    }

    private void OnDistanceChanged(float previousValue, float newValue)
    {
        //Debug.Log($"{OwnerClientId} Distance to CP changed from {previousValue} to {newValue}");
        //checkRacePosition();
    }

    private void OnCPChanged(int previousValue, int newValue)
    {
        //Debug.Log($"{OwnerClientId} Current CP changed from {previousValue} to {newValue}");
        //checkRacePosition();
    }

    public void UpdateRacePosition(int position)
    {
        racePosText.text = "POS : " + position.ToString() + "/2";
    }

    public void checkRacePosition()
    {
        if (!IsOwner) return;
        int othersCP = checkOtherCP();
        float othersDistance = checkOtherDistance();
        
        if (othersDistance != -1 && othersCP > distanceToCP.Value)
        {
            racePosText.text = "POS: 2/2";
            return;
        }
        else if (othersDistance < distanceToCP.Value)
        {
            racePosText.text = "POS: 1/2";
            return;
        }

        if (othersDistance != -1 && othersDistance <= distanceToCP.Value)
        {
            racePosText.text = "POS: 2/2";
        }
        else if (othersDistance >= distanceToCP.Value)
        {
            racePosText.text = "POS: 1/2";
        }

    }

    public float checkOtherDistance()
    {
        if (!IsOwner) return -1;

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            // Skip the current client (owner)
            if (client.Key == NetworkManager.Singleton.LocalClientId)
                continue;

            var playerNetworkObject = client.Value.PlayerObject.GetComponent<Checkpoints>();
            if (playerNetworkObject != null)
            {
                return playerNetworkObject.distanceToCP.Value;
            }
        }

        return -1;
    }

    public int checkOtherCP()
    {
        if (!IsOwner) return -1;

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            // Skip the current client (owner)
            if (client.Key == NetworkManager.Singleton.LocalClientId)
                continue;

            var playerNetworkObject = client.Value.PlayerObject.GetComponent<Checkpoints>();
            if (playerNetworkObject != null)
            {
                return playerNetworkObject.currentCP.Value;
            }
        }

        return -1;
    }

    void Start()
    {
        if (!IsOwner) return;

        //raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        //raceManager.AddPlayer(this);
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
        currentLap = 1;
        //LapUIManager.AddToLap(currentLap);
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
            currentLap += 1;
            currentCheckpointInt = 0;
            //LapUIManager.AddToLap(currentLap);
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
            //else if (other == trackColliders[trackColliders.Length - 1] && currentCheckpoint == trackColliders[i])
            //{
            //    Debug.Log("3rd");
            //    DestroyObject(tempGreenCp);
            //    DestroyObject(tempYellowCp);
            //    tempGreenCp = Instantiate(greenCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
            //    tempYellowCp = Instantiate(tempYellowCp, trackColliders[1].transform.position - checkPointOffset, trackColliders[1].transform.rotation);
            //}
            else if (other == trackColliders[trackColliders.Length - 2] && currentCheckpoint == trackColliders[trackColliders.Length - 2])
            {
                DestroyObject(tempGreenCp);
                DestroyObject(tempYellowCp);
                tempGreenCp = Instantiate(greenCp, trackColliders[trackColliders.Length - 1].transform.position - checkPointOffset, trackColliders[trackColliders.Length - 1].transform.rotation);
                tempYellowCp = Instantiate(yellowCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
                currentCheckpoint = trackColliders[trackColliders.Length - 1];
            }
            //else if (other == trackColliders[trackColliders.Length - 1] && currentCheckpoint == trackColliders[i])
            //{
            //    Debug.Log("3rd");
            //    DestroyObject(tempGreenCp);
            //    DestroyObject(tempYellowCp);
            //    tempGreenCp = Instantiate(greenCp, trackColliders[trackColliders.Length - 1].transform.position - checkPointOffset, trackColliders[trackColliders.Length - 1].transform.rotation);
            //    tempYellowCp = Instantiate(yellowCp, trackColliders[0].transform.position - checkPointOffset, trackColliders[0].transform.rotation);
            //    currentCheckpoint = trackColliders[0];
            //}


            //if (other == trackColliders[i + 1] && currentCheckpoint == trackColliders[i])
            //{
            //    currentCheckpoint = trackColliders[i + 1];
            //}
        }

    }



    void FixedUpdate()
    {
        if (!IsOwner) return;

        // Update the race position based on the NetworkVariable
        //if (NetworkManager.Singleton.IsClient)
        //{
        //    var playerData = GetComponent<Checkpoints>();
        //    racePosText.text = $"Position: {playerData.racePosition.Value}";
        //}

        distanceToCP.Value = Vector3.Distance(trackColliders[currentCheckpointInt].transform.position, transform.position);
        currentCP.Value = currentCheckpointInt;

    }
}
