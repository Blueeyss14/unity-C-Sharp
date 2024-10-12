using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensi;

    Transform playerCamera;
    void Start()
    {
        playerCamera = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraFN();
    }

    void CameraFN() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        playerCamera.Rotate(Vector3.up, mouseX);
    }
}
