/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that makes the disc work and add score to the game.
 */

using UnityEngine;

public class Disc : MonoBehaviour
{
    private bool _active = true;
    
    public void OnEnable()
    {
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.StartRound, OnStartRound);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.EndRound, EndRound);
        EventBus.Unsubscribe(GameEvents.StartRound, OnStartRound);
    }

    /// <summary>
    /// Adds points at the end of the round based on what the current level is
    /// </summary>
    private void EndRound()
    {
        if (!_active) return;
        
        if (PlayerData.instance.currLevel > 1)
        {
            PlayerData.instance.currentScore += 100;
        }
        else
        {
            PlayerData.instance.currentScore += 50;
        }
    }

    /// <summary>
    /// Enables the disc at the start of round
    /// </summary>
    private void OnStartRound()
    {
        _active = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables disc at end of round
    /// </summary>
    public void DiscHit()
    {
        _active = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
