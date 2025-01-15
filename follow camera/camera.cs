using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 3f; 
    public float damping = 0.3f;
    public float rotationSpeed = 5f;

    private Vector3 currentVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.RotateAround(target.position, Vector3.up, horizontalInput * rotationSpeed);
        Vector3 desiredPosition = target.position - transform.forward * distance + Vector3.up * height;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, damping);
        transform.LookAt(target.position + Vector3.up * height);
    }
}
