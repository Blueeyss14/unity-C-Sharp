using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform parent;

    [SerializeField] private float mouseSensitivity;

    void Start()
    {
        parent = transform.parent; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraMove();
    }

    private void CameraMove() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
