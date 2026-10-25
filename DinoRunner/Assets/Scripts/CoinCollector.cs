
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int value = 10;

    [Header("Coin Sound")]
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player =
            other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            // Add score
            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddScore(value);
            }

            // Play sound using Player's AudioSource
            AudioSource playerAudio =
                other.GetComponent<AudioSource>();

            if (playerAudio != null && coinSound != null)
            {
                playerAudio.PlayOneShot(coinSound);
            }

            // Remove collected coin
            Destroy(gameObject);
        }
    }
}
