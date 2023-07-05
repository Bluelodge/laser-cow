using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text pointsText;
    public Text statisticsText;
    public GameObject gameCanvas;
    public GameObject pauseCanvas;
    public GameObject winCanvas;

    int totalTime;

    public static int points;
    public static bool winner;

    void Start()
    {
        // Initial values
        points = 0;
        winner = false;
        totalTime = 0;

        // Hide Pause menu, Game canvas and Win notice
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);

        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;

        // Count game time
        StartCoroutine(TimerCoroutine(3));
    }

    void Update()
    {
        // Show Game canvas
        if (CountdownTimer.startGame == true)
        {
            gameCanvas.SetActive(true);

            // Show points in canvas and avoid negative numbers
            if (points <= 0) points = 0;
            pointsText.text = points.ToString();
        }

        // Check if win and show win canvas
        if (points >= 100) Win();

        // Show Pause menu
        if (Input.GetKey(KeyCode.Space))
        {
            pauseCanvas.SetActive(true);
            PauseMenu.pausedGame = true;
            Time.timeScale = 0;

            // Release mouse
            Cursor.lockState = CursorLockMode.None;
        }
        
        // Hide Pause menu
        if (PauseMenu.pausedGame == false)
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;

            // Lock mouse again
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Show Win notice and statistics
    void Win()
    {
        winner = true;
        Time.timeScale = 0;
        print("Winnnnn");

        // In case timscale doesn't work
        PauseMenu.pausedGame = true;

        winCanvas.SetActive(true);

        // Get statistics
        string statistics = $"COWS: {Target.cows}\n" +
                            $"OVNIS: {Target.ovnis}\n" +
                            $"TIME: {totalTime}s\n +" +
                            $"TOTAL SCORE: {points}";
        statisticsText.text = statistics;
        print(statistics);

        // Back to menu
        if (Input.GetKey(KeyCode.M))
        {
            // Release mouse
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Scenes/GameMenu");
            Debug.Log("Loads Menu");
        }

        // Exit game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Game duration
    IEnumerator TimerCoroutine(int initial = 0)
    {
        yield return new WaitForSeconds(initial);
        while (winner == false)
        {
            totalTime++;
            yield return new WaitForSeconds(1);
        }
    }
}
