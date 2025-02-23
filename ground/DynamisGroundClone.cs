using UnityEngine;

public class LoopingPlane : MonoBehaviour
{
    public GameObject planePrefab;
    public Transform player;
    private float planeLength;
    private bool isClone = false;

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
    }

    void Update()
    {
        if (!isClone && player.position.z > transform.position.z)
        {
            ClonePlane();
            isClone = true;
        }
    }

    void ClonePlane()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + planeLength);
        GameObject newPlane = Instantiate(planePrefab, newPosition, Quaternion.identity);

        newPlane.AddComponent<LoopingPlane>();
        newPlane.GetComponent<LoopingPlane>().player = player;
        newPlane.GetComponent<LoopingPlane>().planePrefab = planePrefab;
    }
}
