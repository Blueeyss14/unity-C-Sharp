using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float moveValue;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    [SerializeField] private float jumpForce;
    [SerializeField] private float velocity;
    [SerializeField] private float gravity;

    Animator anime;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anime = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();

        if (Input.GetKey(KeyCode.Mouse0)) { 
            PlayerAttack();
        }
    }

    void PlayerMove() {

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(0, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);


        bool inputLShift = Input.GetKey(KeyCode.LeftShift);
        bool inputRShift = Input.GetKey(KeyCode.RightShift);

        if (Vector3.zero != moveDirection)
        {
            if (inputLShift || inputRShift)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            Idle();
        }

        Vector3 move = moveDirection * moveValue * Time.deltaTime;
        move.y = velocity * Time.deltaTime;
        characterController.Move(move);
        
    }

    private void Idle() { 
        moveValue = 0;
        anime.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        
    }
    private void Walk()
    {
        moveValue = walk;
        anime.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveValue = run;
        anime.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void PlayerJump()
    {
        if (characterController.isGrounded) { 
            velocity = 0;
            bool jumpSpace = Input.GetKey (KeyCode.Space);

            if (jumpSpace) {
                velocity = jumpForce;    
            }
        }

        velocity += gravity  * Time.deltaTime;
        Vector3 jump = new Vector3(0, velocity, 0);
        characterController.Move(jump * Time.deltaTime);

    }
    private void PlayerAttack() {
        anime.SetTrigger("Attack");
    }
}
