using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public float moveSpeed;
    public Vector3 inputWASD;

    private void Update() {
        float inputAD;
        float inputWS;

        inputAD = Input.GetAxis("Horizontal");
        inputWS = Input.GetAxis("Vertical");

        inputWASD = new Vector3(inputAD, 0, inputWS);
    }
}