using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Player Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float calmTime;
    [SerializeField] private float calmVelocity;
    void Start()
    {
        
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3 (horizontalMove, 0f, verticalMove);

        if (moveDirection.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref calmVelocity, calmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }
}
