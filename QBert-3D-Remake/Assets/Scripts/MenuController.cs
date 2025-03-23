/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script with functions for the main buttons on the menus.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Starts game by moving to next level
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
