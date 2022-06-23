using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used to move the player around => functions called by InputManager
 */
public class PlayerMovementManager : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f; //REWORK GRAVITY INTO AN 'ACTOR' component
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 3f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        controller.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
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
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

    }
}
