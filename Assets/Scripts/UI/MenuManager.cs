using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
/// </summary>

public class MenuManager : MonoBehaviour
{
    public void SwitchSceneGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SwitchSceneMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lost");
    }

    public void GameWon()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Won");
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
