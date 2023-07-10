using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("UI Elements")]
    public Text statisticsText;
    public GameObject winCanvas;
    public GameObject pauseCanvas;

    [Header("Sound Effects")]
    public AudioSource sfxBackgroundSound;
    public AudioSource sfxWin;

    public static bool pausedGame = true;
    public static bool finishedGame = false;

    public static int cows = 0;
    public static int ovnis = 0;

    private int finalScore = 0;
    private float gameTime = 0;
    private string statistics = "";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        print("Starting game...");
        Time.timeScale = 1f;
        cows = 0;
        ovnis = 0;

        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;

        // Hide Pause menu and Win notice
        pauseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    void Update()
    {
        // Check amount of cows
        if (cows >= 30)
        {
            StartCoroutine(WinGame(ScoreController.score));
        }

        // Show Pause menu except on win
        if (Input.GetKey(KeyCode.Space) && finishedGame == false)
        {
            PauseGame();
        }

        // Actions after winning level
        if (finishedGame == true)
        {
            // Back to menu
            if (Input.GetKey(KeyCode.M))
            {
                // Release mouse
                Cursor.lockState = CursorLockMode.None;

                // Reset win
                finishedGame = false;

                SceneManager.LoadScene("Scenes/GameMenu");
                Debug.Log("Loads Menu");
            }

            // Exit game
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    // Start mechanics
    public void BeginGame()
    {
        // Run
        pausedGame = false;
        Debug.Log("You can play!");

        // Enable Game Canvas
        ScoreController.Instance.ShowScoreCanvas();

        // Spawn targets
        StartCoroutine(SpawnTarget.Instance.SpawnTargets());

        // Start game counter
        CounterController.Instance.StartTimer();
    }

    // Pause mechanics
    public void PauseGame()
    {
        // Stop time
        pausedGame = true;
        Time.timeScale = 0;

        // Release mouse
        Cursor.lockState = CursorLockMode.None;

        // Enable Pause Menu Canvas
        pauseCanvas.SetActive(true);

        // Pause backgorund noise
        sfxBackgroundSound.Pause();
    }

    // Continue mechanics
    public void ContinueGame()
    {
        // Run time
        pausedGame = false;
        Time.timeScale = 1;

        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;

        // Disable Pause Menu Canvas
        pauseCanvas.SetActive(false);

        // Unpause background noise
        sfxBackgroundSound.UnPause();
    }

    // Stop mechanics and show info
    public IEnumerator WinGame(int score)
    {
        // Stop background sound
        sfxBackgroundSound.Stop();

        yield return new WaitForSeconds(0.5f);

        // Stop time
        pausedGame = true;
        finishedGame = true;
        Time.timeScale = 0;

        // Disable Game Canvas
        ScoreController.Instance.HideScoreCanvas();

        // Enable Winner Canvas
        winCanvas.SetActive(true);

        // Play win sound effect
        sfxWin.Play();

        // Get statistics
        finalScore = score;
        gameTime = CounterController.Instance.EndTimer();
        statistics = $"COWS: {cows}\n" +
                    $"OVNIS: {ovnis}\n" +
                    $"TIME: {gameTime}s\n" +
                    $"TOTAL SCORE: {finalScore}";

        // Show statics
        statisticsText.text = statistics;

        print("Win!");
    }

}
