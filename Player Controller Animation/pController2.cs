using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    //Jump
    [SerializeField] private float jumpForce;
    [SerializeField] private float velocity;
    [SerializeField] private float gravity;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove() 
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
        else {
            Idle();
        }

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        movement.y = velocity * Time.deltaTime;
        characterController.Move(movement);
    }

    private void Idle() { 
        moveSpeed = 0;
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
    }
    private void Run()
    {
        moveSpeed = runSpeed;
    }

    void PlayerJump() 
    {
        if (characterController.isGrounded)
        { 
            velocity = 0;

            if (Input.GetKey(KeyCode.Space)) 
            { 
                velocity = jumpForce;
            }
        }
        velocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, velocity, 0);
        characterController.Move(jump * Time.deltaTime);

    }

}
