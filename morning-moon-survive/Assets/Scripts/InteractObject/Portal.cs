using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class Portal : MonoBehaviour,IInteractable
{
    [SerializeField] private GameObject targetPortal;
    public void Interact(GameObject player)
    {
      
        LevelManager.Instance.PortalWarp();
        GameManager.Instance.MoveTargetToPoint("Player",targetPortal);
        SaveManager.Instance.SavePlayer();
    }

    public void ShowInteractPrompt()
    {
        // Implement UI or prompt to indicate interaction (e.g., display "Press E to interact")
    }

    public void HideInteractPrompt()
    {
        // Implement UI or prompt to indicate interaction (e.g., display "Press E to interact")
    }
}
