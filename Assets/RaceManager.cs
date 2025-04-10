using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.Services.Relay;
using UnityEngine;

public class RaceManager : NetworkBehaviour
{
    public static RaceManager Instance;

    // List of all players in the game (this could also be a list of NetworkObjects or PlayerData)
    public List<MultiplayerCheckpoints> players = new List<MultiplayerCheckpoints>();
    public List<MultiplayerCheckpoints> playersOld = new List<MultiplayerCheckpoints>();

    //public List<string> playerNames = new List<string>();

    public TextMeshProUGUI racePosText;
    public bool canUpdate = false;
    public bool textInit = false;
    public bool playerInit = false;

    public int lapLimit = 1;
    public int racersFinished;

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
            NetCodeUI.Instance.instructionsUI.SetActive(false);
        }

        if (!canUpdate) return;
        UpdateRacePositions();
    }

    public void SetUpPlayers()
    {
        //Debug.Log("Setting up players");
        players = new List<MultiplayerCheckpoints>();
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            // Get the player's NetworkObject
            var playerNetworkObject = client.Value.PlayerObject;

            var playerCheckpoints = playerNetworkObject.GetComponent<MultiplayerCheckpoints>();
            //var playerName = playerNetworkObject.GetComponent<MultiplayerCarController>();

            if (playerCheckpoints != null)
            {
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
            players[i].transform.GetComponent<MultiplayerCarController>().AllowMovementOnClientsClientRpc();  // i + 1 gives the 1st, 2nd, 3rd, etc. position
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

        checkIfRacerFinished();
        //Used to check if the list has changed based on an old list, if so then return, else update everyones race positions and send the respective position data to all clients
        if (playersOld.Count > 1 &&
        playersOld.Select(p => p.OwnerClientId).SequenceEqual(players.Select(p => p.OwnerClientId))) return;
        
        playersOld = new List<MultiplayerCheckpoints>(players);

        for (int i = 0; i < players.Count; i++)
        {
            //Send player race position data to players for them to display unless they have been announced already
            if (players[i].transform.GetComponent<playerUsername>().positionAnnounced.Value == false)
            {
                players[i].UpdateRacePositionOnClientsClientRpc(i + 1, players.Count);
            }
        }
    }

    public void checkIfRacerFinished()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].currentLap.Value > lapLimit)
            {
                if (players[i].transform.GetComponent<playerUsername>().positionAnnounced.Value == true) return;
                racersFinished++;
                AnnounceWinner(players[i].transform.GetComponent<playerUsername>().username.Value.ToString());
                players[i].transform.GetComponent<playerUsername>().positionAnnounced.Value = true;
            }
        }
    }

    private void AnnounceWinner(string name)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].ShowWhoFinishedTheRaceClientRpc(name, racersFinished);
        }
    }



}
