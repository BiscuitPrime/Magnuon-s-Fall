using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 *  This class is used by the player as its input manager, that will manage the player's inputs and call the adequate functions in other scripts
 *  author : Henri 'Biscuit Prime' Nomico
 */
public class InputManager : MonoBehaviour
{
    #region VARIABLES
    private PlayerInput input; //The player's input system
    private PlayerInput.OnFootActions onFootActions;
    private PlayerMovementManager playerMovementManager;
    private PlayerLookManager playerLookManager;
    #endregion

    void Awake()
    {
        input = new PlayerInput();
        onFootActions = input.OnFoot;
        playerMovementManager = GetComponent<PlayerMovementManager>();
        onFootActions.Jump.performed += ctx => playerMovementManager.Jump(); //everytime the jump input is performed, we call the jump() function throught a callback context
        playerLookManager = GetComponent<PlayerLookManager>();
        onFootActions.Running.started += ctx => playerMovementManager.Run(true);
        onFootActions.Running.canceled += ctx => playerMovementManager.Run(false);
    }

    // FIXEDUPDATE FOR PHYSICS MOVEMENTS
    private void FixedUpdate()
    {
        playerMovementManager.Move(onFootActions.Movement.ReadValue<Vector2>());
    }

    // LATEUPDATE FOR CAMERA MOVEMENTS
    private void LateUpdate()
    {
        playerLookManager.Look(onFootActions.Look.ReadValue<Vector2>());
    }


    //we enable the player's input system when the player is enabled itself :
    private void OnEnable()
    {
        onFootActions.Enable();
    }

    //we disable the player's input system when the player is disabled itself :
    private void OnDisable()
    {
        onFootActions.Disable();
    }
}
