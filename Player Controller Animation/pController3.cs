using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    CharacterController characterController;
    [SerializeField] private float moveValue;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    //jump meloncad
    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    //amimator
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        PlayerMove();
        Jump();

        if (Input.GetKey(KeyCode.Mouse0)) {
            Attack();
        }
    }

    void PlayerMove() {
        float inputVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,inputVertical);
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

        Vector3 movement = moveDirection * moveValue * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime;
        characterController.Move(movement);

    }

    private void Idle() { 
        moveValue = 0;
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void Walk() { 
        moveValue = walk;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveValue = run;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    void Jump() { 
        if (characterController.isGrounded)
        {
            jumpVelocity = 0;
            if (Input.GetKey(KeyCode.Space)) {
                jumpVelocity = jumpValue;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, jumpVelocity, 0);
        characterController.Move(jump * Time.deltaTime);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");            
    }
}