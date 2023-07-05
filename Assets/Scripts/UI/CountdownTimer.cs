using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("UI Elements")]
    public Text counterText;
    public GameObject counterCanvas;

    public static bool startGame;

    float currentTime = 0f;
    float startingTime = 4f;

    void Start()
    {
        print("Starting game");

        startGame = false;
        currentTime = startingTime;

        StartCoroutine(TimerCoroutine());
    }

    // Countdown
    IEnumerator TimerCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime -= 1;
            counterText.text = currentTime.ToString();
            Debug.Log("Waiting one second...");

            yield return new WaitForSeconds(1);
        }

        if (currentTime <= 0)
        {
            // Hide and disable canvas
            counterCanvas.SetActive(false);
            startGame = true;

            // Run
            Time.timeScale = 1f;
            Debug.Log("You can play!");
        }
    }
}
