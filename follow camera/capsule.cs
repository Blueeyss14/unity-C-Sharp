using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float verticalMove = 5f;
    public float horizontalMove = 5f;
    private Vector3 movement;

    public Transform cameraTransform;

    private float rotationSpeed = 700f;

    void Update()
    {
        movement = Vector3.zero;

        if (Input.GetKey("w"))
        {
            verticalMove = 5;
        }
        else if (Input.GetKey("s"))
        {
            verticalMove = -5;
        }
        else { 
            verticalMove = 0;
        }

        movement = new Vector3(0, 0, verticalMove * Time.deltaTime);
        movement = cameraTransform.TransformDirection(movement);
        movement.y = 0;

        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown("d"))
        {
            horizontalMove = 5;
            transform.Rotate(0, 90, 0);
        }
        else if (Input.GetKeyDown("a")) {
            horizontalMove = -5;
            transform.Rotate(0, -90, 0);
        }

        transform.Translate(movement);
    }
}
