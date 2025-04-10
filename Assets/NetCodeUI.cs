using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Collections;
using System;

public class NetCodeUI : NetworkBehaviour
{
    public static NetCodeUI Instance;

    [SerializeField] private TMP_InputField joinCodeInput;
    [SerializeField] private TMP_InputField usernameInput;

    [SerializeField] private TextMeshProUGUI roomCode;
    
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    public GameObject networkingUI;
    public GameObject roomCodeUI;
    public GameObject instructionsUI;

    public string username;

    private string roomAllocId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        instructionsUI.SetActive(false);
        hostBtn.onClick.AddListener(() => {
            StartServer();
        });

        clientBtn.onClick.AddListener(() => {
            StartClient();
        });
    }

    public void StartServer()
    {
        StartRelay();
    }

    public async void StartClient()
    {
        if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
        {
            await UnityServices.InitializeAsync();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        string joinCode = joinCodeInput.text;
        username = usernameInput.text;
        JoinAllocation joinAllocation = await JoinRelay(joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        NetworkManager.Singleton.StartClient();
        networkingUI.SetActive(false);
        roomCodeUI.SetActive(false);
    }

  

    private async Task<Allocation> AllocateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4 - 1);
            roomAllocId = allocation.AllocationId.ToString();
            return allocation;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e.Message);
            return default;
        }
    }

    private async Task<JoinAllocation> JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            return joinAllocation;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e.Message);
            return default;
        }

    }
    
    public async void StartRelay()
    {
        if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
        {
            await UnityServices.InitializeAsync();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Allocation allocation = await AllocateRelay();
        string relayJoinCode = await GetRelayJoinCode(allocation);
        username = usernameInput.text;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        roomCode.text = "ROOM CODE: " + relayJoinCode;
        NetworkManager.Singleton.StartHost();
        networkingUI.SetActive(false);
        instructionsUI.SetActive(true);
    }

    private async Task<string> GetRelayJoinCode(Allocation allocation)
    {
        try
        {
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return relayJoinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e.Message);
            return default;
        }

    }


}
