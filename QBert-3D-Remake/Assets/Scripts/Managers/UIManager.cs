using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text livesTMP, scoreTMP, levelTMP, roundTMP;

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
