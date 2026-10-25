
using UnityEngine;
using TMPro;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;

    [Header("UI Settings")]
    public TMP_Text healthText;

    public TMP_Text[] hearts;

    private int currentHealth;
    private bool isDead;
    [Header("Death Animation")]
    public Animator dinoAnimator;
    public float deathDelay = 0.4f;

    void Start()
    {
        Time.timeScale = 1f;

        currentHealth = maxHealth;
        isDead = false;

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " +
                currentHealth + "/" + maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
                continue;

            if (i < currentHealth)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.gray;
            }
        }
    }

    
    
    void Die()
    {
        isDead = true;

        // Stop player controls after death
        PlayerController controller =
            GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        // Find the dinosaur Animator
        if (dinoAnimator == null)
        {
            dinoAnimator = GetComponentInChildren<Animator>();
        }

        // Start death animation
        if (dinoAnimator != null)
        {
            dinoAnimator.SetBool("IsDead", true);
        }

        StartCoroutine(ShowGameOverAfterDeath());
    }

    IEnumerator ShowGameOverAfterDeath()
    {
        // Allow death animation to finish
        yield return new WaitForSeconds(deathDelay);

        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.LogWarning("GameManager not found!");
            Time.timeScale = 0f;
        }
    }


}
