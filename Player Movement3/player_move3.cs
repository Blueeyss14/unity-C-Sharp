using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    CharacterController characterController;
    public float speed = 4f;
    public float rotationSpeed = 200f;
    public float jumpForce = 5f;
    private float verticalVelocity;
    public float gravity = -13f;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        PlayerMoveFn();
        PlayerRotateFn();
        PlayerJumpFn();
    }

    void PlayerMoveFn() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float moveSpeed = speed;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            moveSpeed += 5f;
        }
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        movement.y = verticalVelocity * Time.deltaTime;
        characterController.Move(movement);
    }

    void PlayerRotateFn() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (moveDirection != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        }
    }

    void PlayerJumpFn() {
        if (characterController.isGrounded && Input.GetKey(KeyCode.Space)) {
            verticalVelocity = jumpForce;
        }
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(jumpVector * Time.deltaTime);
    }
}