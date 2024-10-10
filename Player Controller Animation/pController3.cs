using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    [SerializeField] private float move;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    Animator anime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anime = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMoveFn();
        PlayerJumpFn();

        PlayerAttcakFn();
    }


    void PlayerMoveFn() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,verticalInput);

        if (Vector3.zero != moveDirection)
        {
            bool lShift = Input.GetKey(KeyCode.LeftShift);
            bool rShift = Input.GetKey(KeyCode.RightShift);

            if (lShift || rShift)
            {
                PlayerRun();
            }
            else
            {
                PlayerWalk();
            }
        }
        else { 
            PlayerIdle();
        }

        Vector3 playerMove = moveDirection * move * Time.deltaTime;
        playerMove.y = jumpVelocity * Time.deltaTime;
        characterController.Move(playerMove);
    }

    private void PlayerIdle() { 
        move = 0;
        anime.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void PlayerWalk()
    {
        move = walk;
        anime.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);

    }
    private void PlayerRun()
    {
        move = run;
        anime.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
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

    void PlayerAttcakFn() {

        if (Input.GetKey(KeyCode.Mouse0))
        { 
            anime.SetTrigger("Attack");
        }
        if (Input.GetKey(KeyCode.Q)) {
            anime.SetTrigger("AttackQ");
        }

    }
}
