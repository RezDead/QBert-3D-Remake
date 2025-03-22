using System;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int currentScore = 0;
    public int lives = 3;
    public int currLevel = 0;
    public int currRound = 0;
    
    [SerializeField] private int roundsBeforeNextLevel = 5;

    public override void Awake()
    {
        base.Awake();
        
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.StartRound, StartRound);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);
    }

    private void PlayerDeath()
    {
        lives--;
        if (lives <= 0)
        {
            EventBus.Publish(GameEvents.GameOver);
        }
    }

    private void StartRound()
    {
        currRound++;

        if (currRound <= roundsBeforeNextLevel) return;
        
        currRound = 0;
        EventBus.Publish(GameEvents.NextLevel);
    }

    private void NextLevel()
    {
        currLevel++;
        print("Current Level: " + currLevel);
        EventBus.Publish(GameEvents.StartRound);
    }
}
