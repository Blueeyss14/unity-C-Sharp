using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{

    Transform playerRotate;
    [SerializeField] private float mouseSensi;

    void Start()
    {
        playerRotate = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerRotationFn();
    }

    void PlayerRotationFn() { 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        transform.Rotate(Vector3.up,mouseX);
        //clear
    }
}
