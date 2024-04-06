using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction hotbarAction;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        hotbarAction = playerInput.actions.FindAction("Hotbar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
