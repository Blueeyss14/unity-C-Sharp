using UnityEngine;
using System.Collections.Generic;

public class LoopingPlane : MonoBehaviour
{
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
}
