using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pausedGame = false;

    // Back to Main menu
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("Scenes/GameMenu");
        Debug.Log("Loads Menu");
    }

    // Continue game
    public void OnContinueButtonClick()
    {
        pausedGame = false;
    }

    // Reset game
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Avoid bug on restarting level
        pausedGame = false;
    }
}
