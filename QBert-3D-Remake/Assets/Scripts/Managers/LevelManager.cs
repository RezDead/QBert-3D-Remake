using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Enemy Prefabs")] [SerializeField]
    private GameObject redEgg, purpleEgg, wrongWay, slick;

    private GameObject[] _enemyArr;

    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    /// <summary>
    /// Raises the number of cubes completed, if all are completed ends round
    /// </summary>
    public void CubeCompleted()
    {
        _cubesCompleted++;
        
        //All cubes completed
        if (_cubesCompleted == TOTALCUBES)
        {
            EndRound();
        }
    }

    /// <summary>
    /// Reduces the number of cubes that are completed
    /// </summary>
    public void CubeReset()
    {
        _cubesCompleted--;
        
        //Something very wrong has occured
        if(_cubesCompleted < 0)
            Debug.LogWarning("Error with resetting cubes");
    }

    public void StartRound()
    {
        
    }
    
    private void EndRound()
    {
        ResetEnemies();
        PlayerData.instance.NewRound();
    }

    public void ResetEnemies()
    {
        
    }

    private void SpawnEnemies()
    {
        
    }
    
    
}
