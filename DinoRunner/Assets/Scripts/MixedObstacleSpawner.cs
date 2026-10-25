
using UnityEngine;
using System.Collections;

public class MixedObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    public GameObject cactusPrefab;
    public GameObject birdPrefab;

    [Header("Spawn Positions")]
    public float cactusY = -1.75f;
    public float birdY = 1.5f;

    [Header("Spawn Timing")]
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 5f;

    [Header("Bird Chance")]
    [Range(0f, 1f)]
    public float birdChance = 0.35f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float waitTime = Random.Range(
                minSpawnTime, maxSpawnTime
            );

            yield return new WaitForSeconds(waitTime);

            bool spawnBird =
                Random.value < birdChance;

            GameObject prefab =
                spawnBird ? birdPrefab : cactusPrefab;

            // Use the other obstacle if one prefab is missing
            if (prefab == null)
            {
                prefab = spawnBird
                    ? cactusPrefab
                    : birdPrefab;
            }

            if (prefab == null)
                continue;

            float spawnY =
                prefab == birdPrefab ? birdY : cactusY;

            Vector3 spawnPosition = new Vector3(
                transform.position.x,
                spawnY,
                0f
            );

            Instantiate(
                prefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }
}
