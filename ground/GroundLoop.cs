using UnityEngine;

public class LoopingPlane : MonoBehaviour
{
    public Transform player;
    private float planeLength;

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
    }

    void Update()
    {
        // ke depan
        if (player.position.z > transform.position.z + (planeLength / 2))
        {
            transform.position += Vector3.forward * planeLength;
        }

        //ke belakang
        if (player.position.z < transform.position.z - (planeLength / 2))
        {
            transform.position -= Vector3.forward * planeLength;
        }
    }
}
