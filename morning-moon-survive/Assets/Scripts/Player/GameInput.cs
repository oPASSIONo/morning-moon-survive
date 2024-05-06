using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    
    public static GameInput Instance { get; private set; }
    private PlayerInput playerInput;
    private InputAction pause;

    public event EventHandler OnPauseAction;
    public event EventHandler OnInventoryAction;
    private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();

        playerInput.PlayerControls.Pause.performed += Pause_Performed;

        playerInput.PlayerControls.Inventory.performed += Inventory_Performed;

    }

    private void OnDestroy()
    {
        playerInput.PlayerControls.Pause.performed -= Pause_Performed;
        playerInput.PlayerControls.Inventory.performed -= Inventory_Performed;
        
        playerInput.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Inventory_Performed(InputAction.CallbackContext obj)
    {
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovement()
    {
        Vector2 inputVector = playerInput.PlayerControls.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }   
    
}
