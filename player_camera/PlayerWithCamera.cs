using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cam;

    CharacterController playerController;
    Animator playerAnime;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed; //3
    [SerializeField] private float runSpeed; //7

    [Header("Angle Velocity")]
    [SerializeField] private float calmTime; //0.2
    [SerializeField] private float calmVelocity; //0

    [Header("Jump")]
    [SerializeField] private float jumpVelocity; //0
    [SerializeField] private float gravity; // -9.81
    [SerializeField] private float jumpValue; // 4

    private bool isJumping;
    private bool isGrounded;
    private bool isFallin;

    private Vector3 moveDirection = Vector3.zero;

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

    private void PlayerMoveFn()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 inputDirection = forward * verticalMove + right * horizontalMove;
        inputDirection.Normalize();

        if (inputDirection.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                PlayerRun();
            else
                PlayerWalk();

            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref calmVelocity, calmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            PlayerIdle();
        }

        moveDirection.x = inputDirection.x * moveSpeed;
        moveDirection.z = inputDirection.z * moveSpeed;
    }

    private void PlayerIdle()
    {
        playerAnime.SetFloat("Move", 0f, 0.1f, Time.deltaTime);
        moveSpeed = 0f;
    }

    private void PlayerWalk()
    {
        playerAnime.SetFloat("Move", 0.5f, 0.1f, Time.deltaTime);
        moveSpeed = walkSpeed;
    }

    private void PlayerRun()
    {
        playerAnime.SetFloat("Move", 1f, 0.1f, Time.deltaTime);
        moveSpeed = runSpeed;
    }

    private void PlayerJump()
    {
        if (playerController.isGrounded)
        {
            if (!isGrounded)
            {
                isJumping = false;
                isFallin = false;
                playerAnime.SetBool("isJumping", false);
                playerAnime.SetBool("isFallin", false);
            }

            isGrounded = true;
            jumpVelocity = -1f;

            bool isPlayingJumpAnim = playerAnime.GetCurrentAnimatorStateInfo(0).IsName("Jumping");
            bool isPlayingFallAnim = playerAnime.GetCurrentAnimatorStateInfo(0).IsName("Fallin");

            if (Input.GetKeyDown(KeyCode.Space) && !isPlayingJumpAnim && !isPlayingFallAnim)
            {
                jumpVelocity = jumpValue;
                isJumping = true;
                isGrounded = false;
                playerAnime.SetBool("isJumping", true);
            }
        }
        else
        {
            isGrounded = false;

            if (jumpVelocity < 0 && !isFallin)
            {
                isFallin = true;
                playerAnime.SetBool("isFallin", true);
            }
        }

        jumpVelocity += gravity * Time.deltaTime;
        moveDirection.y = jumpVelocity;

        playerController.Move(moveDirection * Time.deltaTime);

        playerAnime.SetBool("isJumping", isJumping);
        playerAnime.SetBool("isGrounded", isGrounded);
        playerAnime.SetBool("isFallin", isFallin);
    }
}
