using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensivity;

    private Transform parent;


    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraRotate();
    }

    private void CameraRotate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
