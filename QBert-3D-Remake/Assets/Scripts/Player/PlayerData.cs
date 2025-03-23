/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Manager for all the main data points and managing level transitions.
 */

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        //Updates rounds to be more readable
        roundsBeforeNextLevel++;
    }
    
    /// <summary>
    /// What happens on player death
    /// </summary>
    private void PlayerDeath()
    {
        lives--;
        if (lives <= 0)
        {
            EventBus.Publish(GameEvents.GameOver);
        }
    }

    /// <summary>
    /// Details what happens on end of round
    /// </summary>
    private void EndRound()
    {
        StartCoroutine(OnEndRound());
    }

    /// <summary>
    /// Details what happens on end of round
    /// </summary>
    private IEnumerator OnEndRound()
    {
        currRound++;
        if (currRound >= roundsBeforeNextLevel)
        {
            currRound = 1;
            SceneManager.LoadScene(currLevel + 1);
            yield return new WaitForNextFrameUnit();
            EventBus.Publish(GameEvents.NextLevel);
        }
        else
        {
            EventBus.Publish(GameEvents.StartRound);
        }
        
        print("CurrentRound: " + currRound);

    }

    /// <summary>
    /// Details what happens on level start
    /// </summary>
    private void NextLevel()
    {
        currLevel++;
        print("Current Level: " + currLevel);
        EventBus.Publish(currLevel > 3 ? GameEvents.Win : GameEvents.StartRound);
    }
}
