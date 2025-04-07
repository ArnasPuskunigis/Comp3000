using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : NetworkBehaviour
{
    public static RaceManager Instance;

    // List of all players in the game (this could also be a list of NetworkObjects or PlayerData)
    public List<Checkpoints> players = new List<Checkpoints>();

    public TextMeshProUGUI racePosText;
    public bool canUpdate = false;
    public bool textInit = false;
    public bool playerInit = false;

    //public override void OnNetworkSpawn()
    //{
    //    if (!IsOwner) return;
    //    PlayerAddedServerRpc();
    //}

    public void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetUpPlayers();
        }

        Debug.Log("Updating");
        UpdateRacePositions();
    }

    public void SetUpPlayers()
    {
        //Debug.Log("Setting up players");
        players = new List<Checkpoints>();
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            // Get the player's NetworkObject
            var playerNetworkObject = client.Value.PlayerObject;

            // Get the Checkpoints component from the player's NetworkObject
            var playerCheckpoints = playerNetworkObject.GetComponent<Checkpoints>();

            if (playerCheckpoints != null)
            {
                // Add the Checkpoints component to your players list
                players.Add(playerCheckpoints);
            }
        }
        Debug.Log("Clients connected: " + NetworkManager.Singleton.ConnectedClients.Count);
        if (NetworkManager.Singleton.ConnectedClients.Count >= 2)
        {
            canUpdate = true;
        }
    }



    private void UpdateRacePositions()
    {
        players.Sort((player1, player2) =>
        {
            // Compare by current checkpoint index — descending order (more checkpoints = ahead)
            int cpComparison = player2.currentCP.Value.CompareTo(player1.currentCP.Value);
            if (cpComparison != 0) return cpComparison;

            // If they're on the same checkpoint, compare by distance — ascending order (less distance = closer = ahead)
            return player1.distanceToCP.Value.CompareTo(player2.distanceToCP.Value);
        });

        // Now, assign race positions (1st, 2nd, 3rd, etc.) to players
        for (int i = 0; i < players.Count; i++)
        {
            // The player's race position will be updated on the server

            players[i].UpdateRacePositionOnClientsClientRpc(i + 1);  // i + 1 gives the 1st, 2nd, 3rd, etc. position
            //players[i].transform.GetComponent<Checkpoints>().racePosText.text = "Pos = " + (i + 1).ToString() + "/2";
            //Debug.Log("HOST" + players[i].OwnerClientId + " Pos = " + (i+1));
        }
    }

    //// RPC to update the position on all clients
    //[ClientRpc]
    //public void UpdateRacePositionOnClientsClientRpc(int newPosition)
    //{
    //    racePosText.text = $"POS : {newPosition}" + "/2";
    //}

    [ServerRpc]
    public void PlayerAddedServerRpc()
    {
        Debug.Log("Client trying to spawn");
        SetUpPlayers();
    }

}
