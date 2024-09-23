using System Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;


    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        MoveFn();
    }

    void MoveFn() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ) {
                Run();
            } else {
                Walk();
            }
        } else {
            Idle();
        }

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime;
        characterController.Move(movement);
    } 

    private void Idle() {
        moveSpeed = 0;
    }

    private void Walk() {
        moveSpeed = walkSpeed;
    }

    private void Run() {
        moveSpeed = runSpeed;
    }

    void JumpFn() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;

            if (Input.GetKey(KeyCode.Space)) {
                jumpVelocity = jumpForce;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, jumpVelocity, 0);
        characterController.Move(jump * Time.deltaTime);
    }

}