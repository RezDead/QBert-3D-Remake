using System;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    private int _score = 0;
    public int currentScore
    {
        get => _score;
        set
        {
            _score = value;
            UIManager.instance.UpdateScore(_score);
        }
    }

    [SerializeField] private int _lives = 3;
    public int lives
    {
        get => _lives;
        set
        {
            _lives = value;
            UIManager.instance.UpdateLives(_lives);
        }
    }
    
    private int _currLevel = 0;
    public int currLevel
    {
        get => _currLevel;
        set
        {
            _currLevel = value;
            UIManager.instance.UpdateLevel(_currLevel);
        }
    }
    
    private int _currRound = 1;
    public int currRound
    {
        get => _currRound;
        set
        {
            _currRound = value;
            UIManager.instance.UpdateRound(_currRound);
        }
    }
    
    
    [SerializeField] private int roundsBeforeNextLevel = 5;
    
    public void OnEnable()
    {
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Unsubscribe(GameEvents.EndRound, EndRound);
        EventBus.Unsubscribe(GameEvents.NextLevel, NextLevel);
    }

    public override void Awake()
    {
        base.Awake();
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
