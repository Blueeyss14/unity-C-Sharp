using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    CharacterController characterController;
    public float speed = 4f;
    public float rotationSpeed = 200f;
    public float jumpForce = 5f;
    private float verticalVelocity;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        MoveFn();
    }

    void MoveFn() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;


        if (moveDirection != Vector3.zero) {
            float moveSpeed = speed;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                moveSpeed += 10f;
            }
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            movement.y = verticalVelocity * Time.deltaTime;
            characterController.Move(movement);
        }
    }
}
