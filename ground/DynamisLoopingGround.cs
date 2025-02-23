using UnityEngine;
using System.Collections.Generic;


// [ backwardPlane (-1 index) - defaultPlane (0) - forwardPlane (+1 index) ]
public class LoopingGround : MonoBehaviour
{

    public GameObject planePrefab;
    public Transform player;
    private float planeLength;
    private static HashSet<int> spawnedIndexes = new HashSet<int>(); // save index to plane position

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;

        // Start from (-1), tengah (0), dan depan (1)
        SpawnPlane(-1);
        SpawnPlane(0);
        SpawnPlane(1);
    }

    void Update()
    {
        int playerIndex = Mathf.RoundToInt(player.position.z / planeLength);

        // next + 2
        if (!spawnedIndexes.Contains(playerIndex + 2) && spawnedIndexes.Contains(playerIndex))
        {
            SpawnPlane(playerIndex + 1);
        }
        // prev - 2
        if (!spawnedIndexes.Contains(playerIndex - 2) && spawnedIndexes.Contains(playerIndex))
        {
            SpawnPlane(playerIndex - 1);
        }
    }

    void SpawnPlane(int newIndex)
    {
        //isClone?
        if (spawnedIndexes.Contains(newIndex)) return;

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newIndex * planeLength);
        GameObject newPlane = Instantiate(planePrefab, newPosition, Quaternion.identity);

        spawnedIndexes.Add(newIndex); // add index to list

        // add new script to the plane
        LoopingGround script = newPlane.AddComponent<LoopingPlane>();
        script.planePrefab = planePrefab;
        script.player = player;
    }


    /*
    
    public GameObject planePrefab;
    public Transform player;
    private float planeLength;

    private static HashSet<int> spawnedIndexes = new HashSet<int>(); // save index to plane position

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
        int index = Mathf.RoundToInt(transform.position.z / planeLength);

        // add index to first list
        spawnedIndexes.Add(index);
    }

    void Update()
    {
        int playerIndex = Mathf.RoundToInt(player.position.z / planeLength);
        int currentIndex = Mathf.RoundToInt(transform.position.z / planeLength);

        // next
        if (!spawnedIndexes.Contains(currentIndex + 1) && playerIndex > currentIndex)
        {
            SpawnPlane(currentIndex + 1);
        }

        // prev
        if (!spawnedIndexes.Contains(currentIndex - 1) && playerIndex < currentIndex)
        {
            SpawnPlane(currentIndex - 1);
        }
    }

    void SpawnPlane(int newIndex)
    {
        // isClone?
        if (spawnedIndexes.Contains(newIndex)) return;

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newIndex * planeLength);
        GameObject newPlane = Instantiate(planePrefab, newPosition, Quaternion.identity);

        // add index to list
        spawnedIndexes.Add(newIndex);

        // add new script to the planeee
        LoopingPlane script = newPlane.AddComponent<LoopingPlane>();
        script.planePrefab = planePrefab;
        script.player = player;
    }

    */
}
