using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCube : MonoBehaviour
{
    public int score = 0;
    
    private int _currColor = 0;
    [SerializeField] private Material[] color;
    [SerializeField] private int maxColor;
    private bool _completed = false;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = color[_currColor];
    }
    
    /// <summary>
    /// Changes the color to the next color and checks if cube completed
    /// </summary>
    public void NextColor()
    {
        if (_currColor == maxColor) return;
        
        _currColor++;
        GetComponent<MeshRenderer>().material = color[_currColor];

        if (_currColor != maxColor) return;
        
        LevelManager.instance.CubeCompleted();
        _completed = true;
    }
    
    /// <summary>
    /// Resets the color to 0 and informs level manager
    /// </summary>
    public void ResetColor()
    {
        _currColor = 0;
        GetComponent<MeshRenderer>().material = color[0];
        
        if (!_completed) return;
        
        LevelManager.instance.CubeReset();
    }
}
