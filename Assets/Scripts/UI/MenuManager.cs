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
    }
    public void EndGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
