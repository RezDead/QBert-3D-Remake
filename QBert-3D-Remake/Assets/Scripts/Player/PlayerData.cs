using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int currentScore = 0;
    public int lives = 3;
    public int currLevel = 0;
    public int currRound = 0;
    
    [SerializeField] private int roundsBeforeNextLevel = 0;

    public void NewRound()
    {
        currRound++;
        if (currRound > roundsBeforeNextLevel)
        {
            currRound = 0;
            currLevel++;
        }
    }
}
