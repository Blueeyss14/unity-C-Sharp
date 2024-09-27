using System Collection;
using System.Collection.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    //jump---------------------------------
    [SerializeField] private float jumpForce;
    [SerializeField] private float velocity;
    [SerializeField] private float gravity; //-9.81

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        Movement();
        Jump();
    }

    void Movement() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector moveDirection = new Vector3(0, 0, verticalInput);
        moveDirection = transform.TransfomrDirection(moveDirection);

        if (moveDirection != Vector3.zero) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                Run();
            } else {
                Walk();
            }
        } else {
            Idle();
        }
        Vector3 movement = new moveDirection * moveSpeed * Time.deltaTime;
        movement.y = velocity * Time.deltaTime;
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

    void Jump() {

        if (characterController.isGrounded) {
            velocity = 0;
            
            if (Input.GetKey(KeyCode.Space))  {
                velocity = jumpForce;
            }
        }

        velocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, velocity, 0);
        characterController.Move(jump * Time.deltaTime);
    }

}