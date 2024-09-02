using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    CharacterController characterController;
    public float speed = 4f;
    public float rotationSpeed = 200f;
    public float jumpForce = 10f;
    private float verticalVelocity;
    public float gravity = -9.8f;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        Move();
        Rotate();
        Jump();
    }

    void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            float moveSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                moveSpeed += 10f;
            }
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            movement.y = verticalVelocity * Time.deltaTime; // add vertikalVelocity pada pergerakan
            characterController.Move(movement);
        }
    }

    void Rotate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        }
    }

    void Jump() {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(jumpVector * Time.deltaTime);
    }
}
