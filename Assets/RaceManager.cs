using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RaceManager : NetworkBehaviour
{
    public static RaceManager Instance;

    // List of all players in the game (this could also be a list of NetworkObjects or PlayerData)
    public List<Checkpoints> players = new List<Checkpoints>();
    public List<Checkpoints> playersOld = new List<Checkpoints>();

    public TextMeshProUGUI racePosText;
    public bool canUpdate = false;
    public bool textInit = false;
    public bool playerInit = false;

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

        if (!canUpdate) return;
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
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.GetComponent<CarController>().AllowMovementOnClientsClientRpc();  // i + 1 gives the 1st, 2nd, 3rd, etc. position
        }

    }
    private void UpdateRacePositions()
    {

        players.Sort((player1, player2) =>
        {
            int lapComparison = player2.currentLap.Value.CompareTo(player1.currentLap.Value);
            if (lapComparison != 0) return lapComparison;

            int cpComparison = player2.currentCP.Value.CompareTo(player1.currentCP.Value);
            if (cpComparison != 0) return cpComparison;

            return player1.distanceToCP.Value.CompareTo(player2.distanceToCP.Value);
        });

        if (playersOld.Count > 1 &&
        playersOld.Select(p => p.OwnerClientId).SequenceEqual(players.Select(p => p.OwnerClientId))) return;
        
        playersOld = new List<Checkpoints>(players);

        for (int i = 0; i < players.Count; i++)
        {
            //Send player race position data to players for them to display
            players[i].UpdateRacePositionOnClientsClientRpc(i + 1, players.Count);
        }
    }

}
