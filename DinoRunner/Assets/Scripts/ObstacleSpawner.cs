
using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject cactusPrefab;

    [Header("Spawn Timing")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(waitTime);

            if (cactusPrefab != null)
            {
                Instantiate(cactusPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
