
using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab;

    [Header("Spawn Timing")]
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 6f;

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            float waitTime = Random.Range(
                minSpawnTime, maxSpawnTime
            );

            yield return new WaitForSeconds(waitTime);

            if (coinPrefab != null)
            {
                Instantiate(
                    coinPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
    }
}
