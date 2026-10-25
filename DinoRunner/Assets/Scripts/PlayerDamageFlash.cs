
using UnityEngine;
using System.Collections;

public class PlayerDamageFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    public float flashDuration = 1f;
    public float flashInterval = 0.1f;
    public Color flashColor = Color.red;

    private SpriteRenderer[] bodyParts;
    private Color[] originalColors;
    private Coroutine flashRoutine;

    void Awake()
    {
        // Find all visible parts of the dinosaur
        bodyParts = GetComponentsInChildren<SpriteRenderer>();

        originalColors = new Color[bodyParts.Length];

        for (int i = 0; i < bodyParts.Length; i++)
        {
            originalColors[i] = bodyParts[i].color;
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            RestoreColors();
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        float elapsed = 0f;
        bool showRed = true;

        while (elapsed < flashDuration)
        {
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].color = showRed
                    ? flashColor
                    : originalColors[i];
            }

            showRed = !showRed;

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        RestoreColors();
        flashRoutine = null;
    }

    void RestoreColors()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = originalColors[i];
        }
    }

    void OnDisable()
    {
        RestoreColors();
    }
}
