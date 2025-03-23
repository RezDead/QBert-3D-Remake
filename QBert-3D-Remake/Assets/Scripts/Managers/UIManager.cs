/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that handles all the updating for UI.
 */

using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text livesTMP, scoreTMP, levelTMP, roundTMP;
    
    /// <summary>
    /// Updates all on start
    /// </summary>
    private void Start()
    {
        UpdateAll();
    }
    
    /// <summary>
    /// Triggers update for all displays
    /// </summary>
    private static void UpdateAll()
    {
        PlayerData.instance.currentScore = PlayerData.instance.currentScore;
        PlayerData.instance.lives = PlayerData.instance.lives;
        PlayerData.instance.currLevel = PlayerData.instance.currLevel;
        PlayerData.instance.currRound = PlayerData.instance.currRound;
    }

    /// <summary>
    /// Updates the score display
    /// </summary>
    /// <param name="score">Current score</param>
    public void UpdateScore(int score)
    {
        scoreTMP.text = "Score: " + score.ToString();
    }

    /// <summary>
    /// Updates the level display
    /// </summary>
    /// <param name="level">Current level</param>
    public void UpdateLevel(int level)
    {
        levelTMP.text = "Level: " + level.ToString();
    }

    /// <summary>
    /// Updates the lives display
    /// </summary>
    /// <param name="lives">Current Lives</param>
    public void UpdateLives(int lives)
    {
        livesTMP.text = "Lives: " + lives.ToString();
    }

    /// <summary>
    /// Updates the round display
    /// </summary>
    /// <param name="round">Current round</param>
    public void UpdateRound(int round)
    {
        roundTMP.text = "Round: " + round.ToString();
    }
    
}
