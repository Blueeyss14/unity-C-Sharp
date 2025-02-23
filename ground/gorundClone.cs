using UnityEngine;

public class InfinitePlaneSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject planePrefab;
    private float planeLength;
    private float lastZPos;

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
        lastZPos = transform.position.z;
    }

    void Update()
    {

        if (player.position.z > lastZPos + (planeLength / 2))
        {
            lastZPos += planeLength;
            Instantiate(planePrefab, new Vector3(0, 0, lastZPos), Quaternion.identity);
        }

        if (player.position.z < lastZPos - (planeLength / 2))
        {
            lastZPos -= planeLength;
            Instantiate(planePrefab, new Vector3(0, 0, lastZPos), Quaternion.identity);
        }
    }
}
