using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float moveValue;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    Animator playerAnime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnime = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();

        if (Input.GetKey(KeyCode.Mouse0)) { 
            PlayerAttack();
        }
    }

    void PlayerMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3 (0, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero) {
            bool LSpeed = Input.GetKey(KeyCode.LeftShift);
            bool RSpeed = Input.GetKey(KeyCode.RightShift);

            if (LSpeed || RSpeed)
            {
                PlayerRun();
            }
            else { 
                PlayerWalk();
            }
        } else
        {
            PlayerIdle();
        }

        Vector3 movement = moveDirection * moveValue * Time.deltaTime;
        movement.y = jumpVelocity * Time.deltaTime;
        characterController.Move(movement);

    }

    private void PlayerIdle() { 
        moveValue = 0;
        playerAnime.SetFloat("Movement", 0, 0.1f, Time.deltaTime);
    }
    private void PlayerWalk()
    {
        moveValue = walk;
        playerAnime.SetFloat("Movement", 0.5f, 0.1f, Time.deltaTime);
    }
    private void PlayerRun()
    {
        moveValue = run;
        playerAnime.SetFloat("Movement", 1, 0.1f, Time.deltaTime);
    }

    void PlayerJump() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;
            bool playerJump = Input.GetKey(KeyCode.Space);

            if (playerJump) { 
                jumpVelocity = jumpValue;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0,jumpVelocity,0);
        characterController.Move(jump * Time.deltaTime);
    }

    void PlayerAttack() {
        playerAnime.SetTrigger("Attack");
    }
}
