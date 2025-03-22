using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCube : MonoBehaviour
{
    public int score = 25;
    
    private int _currColor = 0;
    [SerializeField] private Material[] color;
    [SerializeField] private int maxColor;
    private bool _completed = false;
    [SerializeField] private bool loop;

    private void Awake()
    {
        EventBus.Subscribe(GameEvents.EndRound, NextRound);
    }
    
    private void Start()
    {
        GetComponent<MeshRenderer>().material = color[_currColor];
    }

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
        if (_currColor != maxColor)
        {
            if (loop && _completed)
            {
                ResetColor();
            }
            else
            {
                _currColor++;
                GetComponent<MeshRenderer>().material = color[_currColor];
            }
        }
        
        if (_currColor != maxColor) return;
        if (_completed) return;
        
        StartCoroutine(LevelManager.instance.CubeCompleted());
        _completed = true;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            NextColor();
        
        if (other.gameObject.CompareTag("Slick"))
            ResetColor();
    }
}
