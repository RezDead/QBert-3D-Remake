using System;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int currentScore = 0;
    public int lives = 3;
    public int currLevel = 0;
    public int currRound = 1;
    
    [SerializeField] private int roundsBeforeNextLevel = 5;

    public override void Awake()
    {
        base.Awake();
        
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);

        roundsBeforeNextLevel++;
    }

    private void PlayerDeath()
    {
        lives--;
        if (lives <= 0)
        {
            EventBus.Publish(GameEvents.GameOver);
        }
    }

    private void EndRound()
    {
        currRound++;
        if (currRound >= roundsBeforeNextLevel)
        {
            currRound = 1;
            EventBus.Publish(GameEvents.NextLevel);
        }
        else
        {
            EventBus.Publish(GameEvents.StartRound);
        }
        
        print("CurrentRound: " + currRound);

    }

    private void NextLevel()
    {
        currLevel++;
        print("Current Level: " + currLevel);
        EventBus.Publish(GameEvents.StartRound);
    }
}
