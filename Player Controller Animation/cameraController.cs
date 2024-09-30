using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform parent;
    [SerializeField] private float mouseSensivity;
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
