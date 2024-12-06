using System;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public static InputReceiver Instance;

    private PlayerInput playerInput;

    private IPlayerCombat playerCombat;
    private IPlayerMovement playerMovement;

    private void Awake()
    {
        Instance = this;

        playerCombat = GetComponent<IPlayerCombat>();
        playerMovement = GetComponent<IPlayerMovement>();

        playerInput = new PlayerInput();

        playerInput.PlayerActions.Enable();
    }

    private void Start()
    {
        playerInput.PlayerActions.LightAttack.performed += OnLightAttackPerformed;
        playerInput.PlayerActions.HeavyAttack.performed += OnHeavyAttackPerformed;
        playerInput.PlayerActions.Dodge.performed += OnDodgePerformed;
        playerInput.PlayerActions.Jump.performed += OnJumpPerformed;
    }

    private void OnLightAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerCombat?.OnLightAttackPerformed();
    }

    private void OnHeavyAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerCombat?.OnHeavyAttackPerformed();
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerMovement?.OnJumpPerformed();
    }

    private void OnDodgePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerMovement?.OnDodgePerformed();
    }



    public float GetMoveDirection()
    {
        return playerInput.PlayerActions.Movement.ReadValue<float>();
    }
}
