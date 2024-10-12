using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    [SerializeField] private float inMove;
    [SerializeField] private float inWalk;
    [SerializeField] private float inRun;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    Animator playerAnimator;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovemnet();
        PlayerJump();
        PlayerAttack();
    }

    void PlayerMovemnet() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (Vector3.zero != moveDirection)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                RunFn();
            }
            else
            {
                WalkFn();
            }
        }
        else {
            IdleFn();
        }

        Vector3 move = moveDirection * inMove * Time.deltaTime;
        move.y = jumpVelocity * Time.deltaTime;
        characterController.Move(move);
    }

    private void IdleFn() { 
        inMove = 0;
        playerAnimator.SetFloat("Player Move", 0, 0.1f, Time.deltaTime);

    }
    private void WalkFn()
    {
        inMove = inWalk;
        playerAnimator.SetFloat("Player Move", 0.5f, 0.1f, Time.deltaTime);
    }
    private void RunFn()
    {
        inMove = inRun;
        playerAnimator.SetFloat("Player Move", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJump() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;

            if (Input.GetKey(KeyCode.Space))
            {
                jumpVelocity = jumpValue;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0,jumpVelocity,0);
        characterController.Move(jump * Time.deltaTime);
    }

    void PlayerAttack() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            playerAnimator.SetTrigger("Player Attack");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            playerAnimator.SetTrigger("Player AttackQ");
        }
    }
}
