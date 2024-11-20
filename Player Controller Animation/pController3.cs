using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;
    [SerializeField] private float movePl;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpForce;
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
        PlayerMoveFn();
        JumpMelonchad();
        PlayerAttackFn();
    }

    void PlayerMoveFn() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0,0,verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);

        if (Vector3.zero != moveDirection)
        {
            bool lShift = Input.GetKey(KeyCode.LeftShift);
            bool rShift = Input.GetKey(KeyCode.RightShift);

            if (lShift || rShift)
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

        Vector3 moveV = moveDirection * movePl * Time.deltaTime;
        moveV.y = jumpVelocity * Time.deltaTime;
        characterController.Move(moveV);

    }

    private void Idle() { 
        movePl = 0;
        playerAnime.SetFloat("Player Animation", 0, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        movePl = walk;
        playerAnime.SetFloat("Player Animation", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        movePl = run;
        playerAnime.SetFloat("Player Animation", 1, 0.1f, Time.deltaTime);
    }

    void JumpMelonchad() {
        if (characterController.isGrounded) { 
            jumpVelocity = 0;

            bool jumpSpace = Input.GetKey(KeyCode.Space);
            if (jumpSpace) {
                jumpVelocity = jumpForce;
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0,jumpVelocity,0);
        characterController.Move(jump * Time.deltaTime);
    }

    void PlayerAttackFn() {

        if (Input.GetKey(KeyCode.Mouse0)) { 
            playerAnime.SetTrigger("Attack");
        }

    }
}
