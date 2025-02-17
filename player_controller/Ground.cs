using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiaraNova : MonoBehaviour
{
    CharacterController playerController;

    [Header("Grounded")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpValue;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isGround = CheckIfGrounded();

        if (isGround && jumpVelocity < 0)
        {
            jumpVelocity = 0;
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, jumpVelocity, 0);
        playerController.Move(jump * Time.deltaTime);
    }

    bool CheckIfGrounded()
    {
        Vector3 origin = transform.position;
        float distance = 0.2f;
        return Physics.Raycast(origin, Vector3.down, distance, groundLayer);
    }
}
