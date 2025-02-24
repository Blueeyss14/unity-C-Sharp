using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LoopingPlane : MonoBehaviour
{
    public GameObject planePrefab;
    public Transform player;
    public TextMeshProUGUI indexText;
    private float planeLength;
    private static HashSet<int> spawnedIndexes = new HashSet<int>(); // save index to plane position
    private static List<GameObject> spawnedPlanes = new List<GameObject>();

    void Start()
    {
        planeLength = GetComponent<Renderer>().bounds.size.z;
        spawnedPlanes.Add(gameObject);
        SpawnPlane(-1);
        SpawnPlane(1);
    }

    void Update()
    {
        int playerIndex = Mathf.RoundToInt(player.position.z / planeLength);

        // character grounded in index then show the text
        if (indexText != null)
        {
            indexText.text = "Index: " + playerIndex;
        }

        // Z Axis
        if (!spawnedIndexes.Contains(playerIndex + 2) && spawnedIndexes.Contains(playerIndex))
        {
            SpawnPlane(playerIndex + 1);
        }

        // -Z Axis
        if (!spawnedIndexes.Contains(playerIndex - 2) && spawnedIndexes.Contains(playerIndex))
        {
            SpawnPlane(playerIndex - 1);
        }

        CleanupPlanes(playerIndex);
    }

    void SpawnPlane(int newIndex)
    {
        if (spawnedIndexes.Contains(newIndex)) return;

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newIndex * planeLength);
        GameObject newPlane = Instantiate(planePrefab, newPosition, Quaternion.identity);

        spawnedIndexes.Add(newIndex);
        spawnedPlanes.Add(newPlane);

        LoopingPlane script = newPlane.AddComponent<LoopingPlane>();
        script.planePrefab = planePrefab;
        script.player = player;
        script.indexText = indexText;
    }

    void CleanupPlanes(int playerIndex)
    {
        spawnedPlanes.RemoveAll(plane =>
        {
            int planeIndex = Mathf.RoundToInt(plane.transform.position.z / planeLength);
            if (planeIndex < playerIndex - 2 || planeIndex > playerIndex + 2)
            {
                spawnedIndexes.Remove(planeIndex);
                Destroy(plane);
                return true;
            }
            return false;
        });
    }
}
