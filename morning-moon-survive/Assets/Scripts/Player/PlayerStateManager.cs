using System;
using UnityEngine;

/// <summary>
/// Manages the player's state based on various inputs and events.
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    public enum PlayerState
    {
        Normal,
        Inventory,
        Crafting,
        Workshop,
        Paused,
        Sleep
    }

    public PlayerState currentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentState = PlayerState.Normal;
    }

    public void SetState(PlayerState newState)
    {
        currentState = newState;
        // Notify other systems of the state change if needed
        HandleStateChange();
    }

    public void ToggleCrafting()
    {
        if (currentState == PlayerState.Normal)
        {
            SetState(PlayerState.Crafting);
        }
        else if (currentState == PlayerState.Crafting)
        {
            SetState(PlayerState.Normal);
        }
    }
    public void ToggleInventory()
    {
        if (currentState == PlayerState.Normal)
        {
            SetState(PlayerState.Inventory);
        }
        else if (currentState == PlayerState.Inventory)
        {
            SetState(PlayerState.Normal);
        }
    }

    public void TogglePause()
    {
        if (currentState == PlayerState.Normal)
        {
            SetState(PlayerState.Paused);
        }
        else if (currentState == PlayerState.Paused)
        {
            SetState(PlayerState.Normal);
        }
    }

    private void HandleStateChange()
    {
        // Add any additional logic needed when the state changes
        // For example, disabling/enabling certain UI elements or functionalities
    }
}