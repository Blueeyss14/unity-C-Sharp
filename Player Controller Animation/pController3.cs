using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    CharacterController characterController;
    [SerializeField] private float move;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    private Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();

        if (Input.GetKey(KeyCode.Mouse0)) { 
            PlayerAttack();
        }
    }

    void PlayerMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(0,0,verticalInput);
        movementDirection = transform.TransformDirection(movementDirection);

        if (movementDirection != Vector3.zero) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                PlayerRun();
            }
            else
            {
                PlayerWalk();
            }
        } else {
            PlayerIdle();
        }

        Vector3 movement = movementDirection * move * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime;
        characterController.Move(movement);
    }

    private void PlayerIdle() 
    { 
        move = 0;
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void PlayerWalk() { 
        move = walk;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void PlayerRun() {
        move = run;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJump() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;

            if (Input.GetKey(KeyCode.Space)) { 
                jumpVelocity = jumpForce;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3 (0, jumpVelocity, 0);
        characterController.Move(jump * Time.deltaTime);
    }

    void PlayerAttack() {
        anim.SetTrigger("Attack");
    }
}