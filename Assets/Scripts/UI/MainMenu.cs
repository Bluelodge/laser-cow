using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start game
    public void OnStartButtonClick()
    {
        Debug.Log("Player started the game");

        // Load Level 1
        SceneManager.LoadScene("Scenes/Level_1");
        PauseMenu.pausedGame = false;
    }

    // Exit Game
    public void OnExittButtonClick()
    {
        Debug.Log("Player left the game");

        // Quit game
        Application.Quit();
    }
}
