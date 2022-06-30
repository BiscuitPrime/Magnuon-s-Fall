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
    private GunManager gunManager;
    #endregion

    void Awake()
    {
        input = new PlayerInput();
        onFootActions = input.OnFoot;
        playerMovementManager = GetComponent<PlayerMovementManager>();
        onFootActions.Jump.performed += ctx => playerMovementManager.Jump(); //everytime the jump input is performed, we call the jump() function throught a callback context
        playerLookManager = GetComponent<PlayerLookManager>();
        onFootActions.Running.started += ctx => playerMovementManager.Run(true); //When the player starts running (shift is pressed), we augment the speed !!! Since it's a ctx, it's called ONCE (when the player starts pressing shift)
        onFootActions.Running.canceled += ctx => playerMovementManager.Run(false); //when the player releases the shift button, we reduce speed !!! Since it's a ctx, it's called ONCE (when the player releases shift)
        gunManager = GetComponent<GunManager>();
        onFootActions.ADS.started += ctx => gunManager.AimDownSight(true); //when the player starts pressing MOUSE RIGHT, he aims down sights
        onFootActions.ADS.canceled += ctx => gunManager.AimDownSight(false); //when the player releases MOUSE RIGHT, he stops aiming down sights
        onFootActions.Reloading.performed += ctx => gunManager.Reload();
        onFootActions.Interact.performed += ctx => playerLookManager.Interact(); //the interact elemnt is contained in the Look manager, to directly connect to the cam
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

    //UPDATE FOR WEAPON/ACTIONS HANDLING
    private void Update()
    {
        if(onFootActions.FiringMain.ReadValue<float>() == 1) //as long as the player is pressing the fire key, the player will fire (automatic)
        {
            gunManager.Fire();
        }
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
