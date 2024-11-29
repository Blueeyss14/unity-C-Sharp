using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController characterController;
    Animator anime;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;


    void Start() {
        characterController = GetComponent<CharacterController>();
        anime = GetComponentInChildren<Animator>();
    }

    void Update() {
        PlayerMove();
        PlayerJump();
        PlayerAttack();
    }

    void PlayerMove() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (Vector3.zero != moveDirection) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                PlayerRun();
            } else {
                PlayerWalk();
            }
        } else {
            PlayerIdle();
        }
        Vector3 finalMove = moveDirection * moveSpeed * Time.deltaTime;
        finalMove.y = jumpVelocity * Time.deltaTime;
        characterController.Move(finalMove);

    }

    private void PlayerIdle() {
        moveSpeed = 0;
        anime.SetFloat("Player Animation", 0,0.1f,Time.deltaTime);
    }
    private void PlayerWalk() {
        moveSpeed = walkSpeed;
        anime.SetFloat("Player Animation", 0.5f,0.1f,Time.deltaTime);
    }
    private void PlayerRun() {
        moveSpeed = runSpeed;
        anime.SetFloat("Player Animation", 1,0.1f,Time.deltaTime);
    }

    void PlayerJump() {
        if (characterController.isGrounded) {
            jumpVelocity = 0;
            if (Input.GetKey(KeyCode.Space)) {
                jumpVelocity = jumpValue;
            }
        }
        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jumpMove = new Vector3(0,jumpVelocity,0);
        characterController.Move(jumpMove * Time.deltaTime);
    }

    void PlayerAttack() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            anime.SetTrigger("Attack");
        }

        if (Input.GetKeyUp(KeyCode.Q)) {
            anime.SetTrigger("AttackQ");
        }
    }
}
