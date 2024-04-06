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
    private void Awake()
    {
        Instance = this;
        
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();

        playerInput.PlayerControls.Pause.performed += Pause_performed;
        
    }
    
    public Vector2 GetMovement()
    {
        Vector2 inputVector = playerInput.PlayerControls.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }   

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
}
