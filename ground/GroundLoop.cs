using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GgroudLoop : MonoBehaviour
{
    public Transform player;
    public float groundLength = 10f;
    private int groundIndex = 0;

    void Update()
    {
        if (player.position.z > transform.position.z + groundLength / 2)
        {
            groundIndex++;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + planeLength);
            Debug.Log("Loop: " + groundIndex);
        }
    }
}
