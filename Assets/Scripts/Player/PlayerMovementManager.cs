using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used to move the player around => functions called by InputManager
 */
public class PlayerMovementManager : MonoBehaviour
{
    #region VARIABLES
    private CharacterController controller;
    private Vector3 velocity;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    private float curSpeed;
    [SerializeField] private float gravity = -9.8f; //REWORK GRAVITY INTO AN 'ACTOR'-handled component
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 3f;
    private GunManager actorGunManager;
    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        actorGunManager = gameObject.GetComponent<GunManager>();
        curSpeed = baseSpeed;
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
    }

    //function that will handle the movement -> should be reworked into a command pattern
    public void Move(Vector2 input)
    {
        Vector3 direction = Vector3.zero;
        direction.x = input.x;
        direction.z = input.y;
        controller.Move(transform.TransformDirection(direction) * curSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    //function that will handle the jump -> should be reworked into a command pattern
    public void Jump()
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -0.6f * gravity);
        }

    }

    //function that will handle the run -> should be reworked into a command pattern
    public void Run(bool isRunning)
    {
        if (isRunning) {
            actorGunManager.getHolder().GetComponent<Animator>().SetBool("isADS", false); //when the player is running, he will automatically stop ADS
            curSpeed = sprintSpeed; 
        }
        else {
            curSpeed = baseSpeed; 
        }
        
    }
}
