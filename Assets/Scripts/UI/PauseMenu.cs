using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Back to Main Menu
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("Scenes/GameMenu");
        Debug.Log("Loads Menu");
    }

    // Continue game
    public void OnContinueButtonClick()
    {
        GameController.Instance.ContinueGame();
    }

    // Reset game
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
