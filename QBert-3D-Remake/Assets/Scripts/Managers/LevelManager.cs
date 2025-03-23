/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that handles most of the game events within the project and manages how objects work with the levels.
 */

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Player Info")] [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] private float respawnTime = 5f;
    public float discUseTime;
    
    private GameObject _player;
    
    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    public Vector3 newWorldZero = new Vector3(100, 100, 100);

    private void OnEnable() 
    {
        EventBus.Subscribe(GameEvents.StartRound, StartRound);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.Win, OnGameEnd);
        EventBus.Subscribe(GameEvents.GameOver, OnGameEnd);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.StartRound, StartRound);
        EventBus.Unsubscribe(GameEvents.EndRound, EndRound);
        EventBus.Unsubscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Unsubscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Unsubscribe(GameEvents.GameOver, OnGameEnd);
        EventBus.Unsubscribe(GameEvents.Win, OnGameEnd);
    }
    
    /// <summary>
    /// Triggers initial level start
    /// </summary>
    private void Start()
    {
        EventBus.Publish(GameEvents.NextLevel);
    }

    /// <summary>
    /// Disables level manager when game is over
    /// </summary>
    private void OnGameEnd()
    {
        EnemyManager.instance.StopSpawning();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Details what happens when the player death event happens
    /// </summary>
    private void PlayerDeath()
    {
        EnemyManager.instance.ResetEnemies(true);
        StartCoroutine(PlayerRespawn());
        StartCoroutine(PlayerDeathEnemyDelay());
    }

    /// <summary>
    /// Delays enemy spawns to player respawn time
    /// </summary>
    private IEnumerator PlayerDeathEnemyDelay()
    {
        EnemyManager.instance.StopSpawning();
        yield return new WaitForSeconds(respawnTime);
        EnemyManager.instance.StartSpawning();
    }

    /// <summary>
    /// Details what happens when the disc used event happens
    /// </summary>
    private void DiscUsed()
    {
        StartCoroutine(OnDiscUsed());
    }

    /// <summary>
    /// What happens on disc used
    /// </summary>
    private IEnumerator OnDiscUsed()
    {
        EnemyManager.instance.StopSpawning();
        EnemyManager.instance.ResetEnemies(false);
        yield return new WaitForSeconds(discUseTime);
        EnemyManager.instance.StartSpawning();
    }

    /// <summary>
    /// Details what happens when the start round event happens
    /// </summary>
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
    
    /// <summary>
    /// Details what happens when the end round event happens
    /// </summary>
    private void EndRound()
    {
        print("End Round");
        _cubesCompleted = 0;
        EnemyManager.instance.ResetEnemies(true);
        EnemyManager.instance.StopSpawning();
        EnemyManager.instance.StartSpawning();
    }
    
    /// <summary>
    /// Respawns the player
    /// </summary>
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
