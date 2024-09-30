using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float move;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpValue;
    [SerializeField] private float velocity;
    [SerializeField] private float gravity;

    private Animator playerAnimator;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();

        if (Input.GetKey(KeyCode.Mouse0)) {
            PlayerAttck();
        }
    }

    void PlayerMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero)
        {
            bool LeftRun = Input.GetKey(KeyCode.LeftShift);
            bool RightRun = Input.GetKey(KeyCode.LeftShift);

            if (LeftRun || RightRun)
            {
                PlayerRun();
            }
            else
            {
                PlayerWalk();
            }
        }
        else { 
            PlayerIdle();
        }

        Vector3 movement = moveDirection * move * Time.deltaTime;
        movement.y = velocity * Time.deltaTime;
        characterController.Move(movement);
    }

    private void PlayerIdle() { 
        move = 0;
        playerAnimator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void PlayerWalk()
    {
        move = walk;
        playerAnimator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void PlayerRun()
    {
        move = run;
        playerAnimator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJump() {
        if (characterController.isGrounded) { 
            velocity = 0;

            bool jumpSpace = Input.GetKey(KeyCode.Space);
            if (jumpSpace)
            {
                velocity = jumpValue;
            }
        }

        velocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0,velocity,0);
        characterController.Move(jump * Time.deltaTime);
    }

    void PlayerAttck() {
        playerAnimator.SetTrigger("Attack");
    }
}
