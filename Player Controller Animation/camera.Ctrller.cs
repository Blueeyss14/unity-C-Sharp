using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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

    void CameraRotate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
