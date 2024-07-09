using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem
{

    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private DamageDealer damageDealer;

        private void OnEnable()
        {
            var playerInput = new PlayerInput();
            playerInput.PlayerControls.Enable();

            playerInput.PlayerControls.Attack.performed += OnAttackPerformed;

        }

        private void OnDisable()
        {
            var playerInput = new PlayerInput();
            playerInput.PlayerControls.Disable();

            playerInput.PlayerControls.Attack.performed -= OnAttackPerformed;
        }

        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
            damageDealer.PerformAttack();
        }

    }
}