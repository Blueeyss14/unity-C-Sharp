using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    Animator animeMove;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animeMove = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovementFn();
        PlayerJumpFn();
        PlayerAttackFn();
    }

    void PlayerMovementFn() {
        float verticalAxis = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3 (0, 0, verticalAxis);
        moveDirection = transform.TransformDirection(moveDirection);

        if (Vector3.zero != moveDirection)
        {

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                PlayerRunFn();
            }
            else
            {
                PlayerWalkFn();
            }
        }
        else { 
            PlayerIdleFn();
        }

        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;
        move.y = jumpVelocity * Time.deltaTime;
        characterController.Move(move);
    }

    private void PlayerIdleFn() {
        moveSpeed = 0;
        animeMove.SetFloat("Move Speed" ,0 ,0.1f , Time.deltaTime);
    }
    private void PlayerWalkFn()
    {
        moveSpeed = walk;
        animeMove.SetFloat("Move Speed", 0.5f, 0.1f, Time.deltaTime);

    }
    private void PlayerRunFn()
    {
        moveSpeed = run;
        animeMove.SetFloat("Move Speed", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJumpFn() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;

            if (Input.GetKey(KeyCode.Space)) {
                jumpVelocity = jumpValue;
            }
        }
        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0,jumpVelocity,0);
        characterController.Move(jump * Time.deltaTime);
    }

    
}
