/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that describes the functionality of the main cubes of the level.
 */

using UnityEngine;

public class LevelCube : MonoBehaviour
{
    [SerializeField] private int score = 25;
    
    private int _currColor = 0;
    [SerializeField] private Material[] color;
    [SerializeField] private int maxColor;
    private bool _completed = false;
    [SerializeField] private bool loop;

    private void OnEnable()
    {
        EventBus.Subscribe(GameEvents.EndRound, NextRound);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.EndRound, NextRound);
    }
    
    /// <summary>
    /// Sets starting color
    /// </summary>
    private void Start()
    {
        GetComponent<MeshRenderer>().material = color[_currColor];
    }

    /// <summary>
    /// Resets the cube to base settings
    /// </summary>
    private void NextRound()
    {
        _currColor = 0;
        _completed = false;
        GetComponent<MeshRenderer>().material = color[0];
    }
    
    /// <summary>
    /// Changes the color to the next color and checks if cube completed
    /// </summary>
    private void NextColor()
    {
        if (loop && _completed)
        {
            ResetColor();
            PlayerData.instance.currentScore += score;
        }
        else if (!_completed)
        {
            _currColor++;
            GetComponent<MeshRenderer>().material = color[_currColor];
            PlayerData.instance.currentScore += score;
            
            if (_currColor == maxColor)
            {
                _completed = true;
                StartCoroutine(LevelManager.instance.CubeCompleted());
            }
        }
    }
    
    /// <summary>
    /// Resets the color to 0 and informs level manager
    /// </summary>
    private void ResetColor()
    {
        if(_currColor == 0) return;
        
        _currColor = 0;
        GetComponent<MeshRenderer>().material = color[0];
        
        if (!_completed) return;
        
        LevelManager.instance.CubeReset();
        _completed = false;
    }

    /// <summary>
    /// Updates cube based on what collided with it
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            NextColor();
        
        if (other.gameObject.CompareTag("Slick"))
            ResetColor();
    }
}
