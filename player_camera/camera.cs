using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 10f;

    private float yawCamY = 0f;
    private float pitchCamX = 15f;
    private float distance;

    void Start()    
    {
        distance = offset.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        yawCamY += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitchCamX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitchCamX = Mathf.Clamp(pitchCamX, -35f, 60f);

        //zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        //rotation
        Quaternion rotation = Quaternion.Euler(pitchCamX, yawCamY, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance + Vector3.up * offset.y;

        transform.position = position;
        transform.rotation = rotation;
    }
}