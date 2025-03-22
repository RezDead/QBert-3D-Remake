using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Player Info")] [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] private float respawnTime = 5f;
    public float discUseTime;
    
    private GameObject _player;

    [HideInInspector]public int discsInScene = 0;
    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    public readonly Vector3 newWorldZero = new Vector3(100, 100, 100);

    private void OnEnable() 
    {
        EventBus.Subscribe(GameEvents.StartRound, StartRound);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.StartRound, StartRound);
        EventBus.Unsubscribe(GameEvents.EndRound, EndRound);
        EventBus.Unsubscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Unsubscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Unsubscribe(GameEvents.NextLevel, NextLevel);
    }
    
    private void Start()
    {
        EventBus.Publish(GameEvents.NextLevel);
    }

    private void PlayerDeath()
    {
        EnemyManager.instance.ResetEnemies(true);
        StartCoroutine(PlayerRespawn());
    }

    private void DiscUsed()
    {
        StartCoroutine(OnDiscUsed());
    }

    private IEnumerator OnDiscUsed()
    {
        EnemyManager.instance.StopSpawning();
        EnemyManager.instance.ResetEnemies(false);
        yield return new WaitForSeconds(discUseTime);
        EnemyManager.instance.StartSpawning();
    }

    private void NextLevel()
    {
        print("Next Level");
    }
    
    private void StartRound()
    {
        print("Start Round");
        if (_player != null)
        {
            Destroy(_player);
        }
        
        _player = Instantiate(playerPrefab, newWorldZero, Quaternion.identity);
        
        EnemyManager.instance.StartSpawning();
    }
    
    private void EndRound()
    {
        print("End Round");
        _cubesCompleted = 0;
        EnemyManager.instance.ResetEnemies(true);
        EnemyManager.instance.StopSpawning();
    }
    
    private IEnumerator PlayerRespawn()
    {
        Destroy(_player);
        yield return new WaitForSeconds(respawnTime);
        _player = Instantiate(playerPrefab, newWorldZero, Quaternion.identity);
    }
    
    /// <summary>
    /// Raises the number of cubes completed, if all are completed ends round
    /// </summary>
    public IEnumerator CubeCompleted()
    {
        _cubesCompleted++;
        print("Cube Completed: " + _cubesCompleted);
        //All cubes completed
        if (_cubesCompleted >= TOTALCUBES)
        {
            Destroy(_player);
            yield return new WaitForNextFrameUnit();
            EventBus.Publish(GameEvents.EndRound);
        }
    }

    /// <summary>
    /// Reduces the number of cubes that are completed
    /// </summary>
    public void CubeReset()
    {
        _cubesCompleted--;
        
        //Something very wrong has occured :(
        if(_cubesCompleted < 0)
            Debug.LogWarning("Error with resetting cubes: " + _cubesCompleted);
    }
    
    /// <summary>
    /// Used to fetch players world position
    /// </summary>
    /// <returns>Players world position</returns>
    public Vector3 ReturnPlayerPosition()
    {
        return _player.transform.position;
    }
    
}
