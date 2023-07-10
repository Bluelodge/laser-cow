using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;

    [Header("UI Elements")]
    public Text scoreText;
    public GameObject gameCanvas;

    public static int score = 0;

    private int cowPoints = 20;
    private int ovniPoints = 20;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        score = 0;
    }

    public void ShowScoreCanvas()
    {
        gameCanvas.SetActive(true);
    }

    public void HideScoreCanvas()
    {
        gameCanvas.SetActive(false);
    }

    // Update Score
    public void AddScore()
    {
        score += cowPoints;
        scoreText.text = score.ToString();

        // Check for maximum score
        if (score >= 500)
        {
            StartCoroutine(GameController.Instance.WinGame(score));
        }
    }

    public void SubtractScore()
    {
        score -= ovniPoints;
        
        // Avoid negative numbers
        if (score <= 0) score = 0;
        scoreText.text = score.ToString();
    }
}
