/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that details what happens on win or lose of the game.
 */

using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void OnEnable()
    {
        EventBus.Subscribe(GameEvents.Win, OnWin);
        EventBus.Subscribe(GameEvents.GameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.Win, OnWin);
        EventBus.Unsubscribe(GameEvents.GameOver, OnGameOver);
    }

    /// <summary>
    /// Loads win scene
    /// </summary>
    private void OnWin()
    {
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Loads loss scene
    /// </summary>
    private void OnGameOver()
    {
        SceneManager.LoadScene(5);
    }
}
