using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravity;

    private Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMoveFn();
        PlayerJumpFn();

        if (Input.GetKeyDown(KeyCode.Mouse0)) { 
            Attack();
        }
    }

    void PlayerMoveFn()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(verticalInput, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Run();
            }
            else
            {
                Walk();
            }
        } else {
            Idle();
        }

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        movement.y = verticalVelocity * Time.deltaTime;
        characterController.Move(movement);
    }

    private void Idle() {
        moveSpeed = 0;
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk() {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f,0.1f, Time.deltaTime);

    }

    private void Run() {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJumpFn()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0;

            if (Input.GetKey(KeyCode.Space)) {
            verticalVelocity = jumpForce;
            }
        }
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(jumpVector * Time.deltaTime);
    }

    private 5 Attack() {
        anim.SetTrigger("Attack");
    } 
}