using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController playerController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float calmTime;
    [SerializeField] private float calmVelocity;


    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMoveFn();
    }

    private void PlayerMoveFn() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0f,verticalMove);

        if (moveDirection.magnitude >= 0.1f) {

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
            else {
                PlayerIdle();
            }


            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref calmVelocity, calmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            playerController.Move(moveDirection.normalized * moveSpeed   * Time.deltaTime);
        }
    }

    private void PlayerIdle() { 
        moveSpeed = 0f;
    }

    private void PlayerWalk()
    {
        moveSpeed = walkSpeed;
    }

    private void PlayerRun() {
        moveSpeed = runSpeed;
    }
}
