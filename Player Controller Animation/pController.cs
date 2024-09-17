using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;


    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        
        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) {
            Walk();
        } else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) {
            Run();
        } else if (moveDirection == Vector3.zero) {
            Idle();
         }


        if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
        }
        
        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void Idle() {
        
    }

    private void Walk() {
        moveSpeed = walkSpeed;
    }

    private void Run() {
        moveSpeed = runSpeed;
    }

    private void Jump() {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    } 
}
