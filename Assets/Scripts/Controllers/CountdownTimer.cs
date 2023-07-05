using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("UI Elements")]
    public Text countdownText;
    public GameObject counterCanvas;

    float currentTime = 0f;
    float startingTime = 4f;

    void Start()
    {
        currentTime = startingTime;

        StartCoroutine(UpdateCountdownCoroutine());
    }

    // Countdown
    IEnumerator UpdateCountdownCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime--;
            countdownText.text = currentTime.ToString();
            Debug.Log("Waiting one second...");

            yield return new WaitForSeconds(1);
        }

        // Disable canvas
        counterCanvas.SetActive(false);

        GameController.Instance.BeginGame();
    }
}
