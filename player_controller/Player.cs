using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController playerController;
    Animator playerAnime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float calmTime;
    [SerializeField] private float calmVelocity;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpValue;

    private bool isJumping;
    private bool isGrounded;
    private bool isFallin;


    void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerAnime = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMoveFn();
        PlayerJump();
    }

    private void PlayerMoveFn() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0f,verticalMove);

        if (moveDirection.magnitude >= 0.1f)
        {

            if (Vector3.zero != moveDirection)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    PlayerRun();
                }
                else
                {
                    PlayerWalk();
                }
            }
            else
            {
                PlayerIdle();
            }

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref calmVelocity, calmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            playerController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        else {
            PlayerIdle();
        }
    }

    private void PlayerIdle() {
        playerAnime.SetFloat("Move",0f, 0.1f, Time.deltaTime);
        playerAnime.SetFloat("speedMultiplier", 0.2f, 0.5f, Time.deltaTime);
        moveSpeed = 0f;
    }

    private void PlayerWalk()
    {
        playerAnime.SetFloat("Move", 0.5f, 0.1f, Time.deltaTime);
        playerAnime.SetFloat("speedMultiplier", 1, 0.5f, Time.deltaTime);
        moveSpeed = walkSpeed;
    }

    private void PlayerRun() {
        playerAnime.SetFloat("Move", 1, 0.1f, Time.deltaTime);
        playerAnime.SetFloat("speedMultiplier", 1.2f, 0.5f, Time.deltaTime);
        moveSpeed = runSpeed;
    }

    private void PlayerJump()
    {
        if (playerController.isGrounded)
        {

                isGrounded = true;
                isJumping = false;
                isFallin = false;

                playerAnime.SetBool("isJumping", isJumping);
                playerAnime.SetBool("isGrounded", isGrounded);
                playerAnime.SetBool("isFallin", isFallin);

            jumpVelocity = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                isGrounded = false;
                isFallin = true;

                playerAnime.SetBool("isJumping", isJumping);
                playerAnime.SetBool("isGrounded", isGrounded);
                playerAnime.SetBool("isFallin", isFallin);

                jumpVelocity = jumpValue;
            }
        }
        else
        {
                isGrounded = false;
                isJumping = false;
                isFallin = true;

                playerAnime.SetBool("isJumping", isJumping);
            playerAnime.SetBool("isGrounded", isGrounded);
            playerAnime.SetBool("isFallin", isFallin);
    }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, jumpVelocity, 0);
        playerController.Move(jump * Time.deltaTime);
    }


}