using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text livesTMP, scoreTMP, levelTMP, roundTMP;

    private void Start()
    {
        UpdateAll();
    }

    private static void UpdateAll()
    {
        PlayerData.instance.currentScore = PlayerData.instance.currentScore;
        PlayerData.instance.lives = PlayerData.instance.lives;
        PlayerData.instance.currLevel = PlayerData.instance.currLevel;
        PlayerData.instance.currRound = PlayerData.instance.currRound;
    }

    public void UpdateScore(int score)
    {
        scoreTMP.text = "Score: " + score.ToString();
    }

    public void UpdateLevel(int level)
    {
        levelTMP.text = "Level: " + level.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesTMP.text = "Lives: " + lives.ToString();
    }

    public void UpdateRound(int round)
    {
        roundTMP.text = "Round: " + round.ToString();
    }
    
}
