using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class JoinCodeDisplay : NetworkBehaviour
{
    [SerializeField] private TMP_Text joinCodeText;

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            joinCodeText.text = HostManager.Instance.JoinCode;
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        
    }
}
