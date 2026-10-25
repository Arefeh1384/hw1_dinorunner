
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;

    private int highScore = 0;

    [Header("UI Settings")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;

    void Start()
    {
        score = 0;

        // Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreUI();

        Debug.Log("Loaded High Score: " + highScore);
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;

            // Save new record
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            Debug.Log("NEW HIGH SCORE: " + highScore);
        }

        UpdateScoreUI();

        Debug.Log("Score: " + score);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "FINAL SCORE: " + score;
        }
    }
}
