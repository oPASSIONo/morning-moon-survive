using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RelayManager : MonoBehaviour
{ 
    
    [SerializeField] private TextMeshProUGUI joinCodeText;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private GameObject control;
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void StartRelay()
    {
        string joinCode = await StartHostWithRelay();
        joinCodeText.text = joinCode;
    }

    public async void JoinRelay()
    {
        await StartClientWithRelay(joinCodeInputField.text);
        control.SetActive(false);
    }

    private async Task<string> StartHostWithRelay(int maxConnection = 4)
    {
        try
        {
           Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);

           RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
           
           NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
           
           string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
           Debug.Log(joinCode);
           
           return NetworkManager.Singleton.StartHost() ? joinCode : null;
           
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay create location failed {e.Message}");
            throw;
        }
    }

    private async Task<bool> StartClientWithRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay with : " + joinCode);
            JoinAllocation joinAllocation =  await RelayService.Instance.JoinAllocationAsync(joinCode);
            
            
            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
           
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
            
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
            throw;
        }
        
    }
    
}
