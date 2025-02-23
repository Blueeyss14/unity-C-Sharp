using UnityEngine;

public class InfinitePlaneSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject planePrefab;
    private float planeLength;
    private float lastZPos;
    private GameObject currentPlane;
    private GameObject nextPlaneForward;
    private GameObject nextPlaneBackward;

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
        lastZPos = transform.position.z;
        currentPlane = gameObject;
    }

    void Update()
    {
        if (player.position.z > lastZPos + (planeLength / 2))
        {
            lastZPos += planeLength;
            if (nextPlaneForward == null)
            {
                nextPlaneForward = Instantiate(planePrefab, new Vector3(0, 0, lastZPos), Quaternion.identity);
            }
            else
            {
                nextPlaneForward.transform.position = new Vector3(0, 0, lastZPos);
            }
            currentPlane = nextPlaneForward;
            nextPlaneBackward = gameObject;
        }

        if (player.position.z < lastZPos - (planeLength / 2))
        {
            lastZPos -= planeLength;
            if (nextPlaneBackward == null)
            {
                nextPlaneBackward = Instantiate(planePrefab, new Vector3(0, 0, lastZPos), Quaternion.identity);
            }
            else
            {
                nextPlaneBackward.transform.position = new Vector3(0, 0, lastZPos);
            }
            currentPlane = nextPlaneBackward;
            nextPlaneForward = gameObject;
        }
    }
}