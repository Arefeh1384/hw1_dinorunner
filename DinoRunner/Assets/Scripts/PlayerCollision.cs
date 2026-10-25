
using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Damage Settings")]
    public float invincibilityTime = 1f;
    [Header("Hit Sound")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    private PlayerHealth playerHealth;
    private PlayerDamageFlash damageFlash;
    private bool isInvincible = false;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        damageFlash = GetComponent<PlayerDamageFlash>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && !isInvincible)
        {
            playerHealth.TakeDamage(1);
            if (damageFlash != null)
            {
                damageFlash.Flash();
            }
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            StartCoroutine(TemporaryInvincibility());
        }
    }

    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
    }
}
