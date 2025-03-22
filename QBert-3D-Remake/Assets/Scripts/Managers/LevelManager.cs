using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    
    [Header("Level Settings")]
    [SerializeField] private LevelSettings[] levelSettings;
    [SerializeField] private RoundSettings[] roundSettings;
    
    [Header("Player Info")] [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] private float respawnTime = 5f;
    
    private GameObject _player;
    
    [Header("Enemy Info")]
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private float enemySpawnRate = 5f;
    
    struct Enemy
    {
        public Enemy(GameObject enemyObject, EnemyTypes type)
        {
            this.enemyObject = enemyObject; this.type = type;
        }
        
        public GameObject enemyObject;
        public EnemyTypes type;
    }
    
    private List<Enemy> _currEnemies = new List<Enemy>();

    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    private Vector3 _newWorldZero = new Vector3(100, 100, 100);

    private void Start()
    {
        EventBus.Subscribe(GameEvents.StartRound, StartRound);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);
        
        
        EventBus.Publish(GameEvents.NextLevel);
    }

    private void PlayerDeath()
    {
        StopCoroutine(SpawnEnemies());
        ResetEnemies(true);
        StartCoroutine(PlayerRespawn());
    }

    private void DiscUsed()
    {
        
    }

    private void NextLevel()
    {
        print("Next Level");
        _player = Instantiate(playerPrefab, _newWorldZero, Quaternion.identity);
    }
    
    public IEnumerator PlayerRespawn()
    {
        Destroy(_player);
        yield return new WaitForSeconds(respawnTime);
        _player = Instantiate(playerPrefab, _newWorldZero, Quaternion.identity);
    }
    
    /// <summary>
    /// Raises the number of cubes completed, if all are completed ends round
    /// </summary>
    public void CubeCompleted()
    {
        _cubesCompleted++;
        
        //All cubes completed
        if (_cubesCompleted == TOTALCUBES)
        {
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

    private void StartRound()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private void EndRound()
    {
        StopCoroutine(SpawnEnemies());
        ResetEnemies(true);
        EventBus.Publish(GameEvents.StartRound);
    }
    
    public void ResetEnemies(bool killPurple)
    {

        Enemy[] enemies = _currEnemies.ToArray();
        List<Enemy> tempCurrEnemies = new List<Enemy>();
        
        
        for (int enemiesProcessed = 0; enemiesProcessed < enemies.Length; enemiesProcessed++)
        {
            if (enemies[enemiesProcessed].type != EnemyTypes.PurpleEgg && !killPurple)
            {
                Destroy(enemies[enemiesProcessed].enemyObject);
            }

            else if (enemies[enemiesProcessed].type == EnemyTypes.PurpleEgg && !killPurple)
            {
                tempCurrEnemies.Add(enemies[enemiesProcessed]);
            }

            else
            {
                Destroy(enemies[enemiesProcessed].enemyObject);
            }
        }
        
        _currEnemies = tempCurrEnemies;
    }

    /// <summary>
    /// Spawns a random enemy based on how often enemy spawn rate is set to.
    /// </summary>
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(enemySpawnRate);

        EnemyTypes enemySpawned = (EnemyTypes)Random.Range(0, 4);

        GameObject spawnedEnemyObj;

        switch (enemySpawned)
        {
            case EnemyTypes.RedEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[0], _newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            case EnemyTypes.PurpleEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[1], _newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.PurpleEgg));
                break;
            case EnemyTypes.WrongWay:
                if (Random.Range(0, 2) == 0)
                {
                    spawnedEnemyObj = Instantiate(enemyTypes[2], new Vector3(93, 93, 100), Quaternion.identity);
                    spawnedEnemyObj.GetComponent<WrongWay>().rightStart = false;
                    _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.WrongWay));
                }
                else
                {
                    spawnedEnemyObj = Instantiate(enemyTypes[2], new Vector3(100, 93, 93), Quaternion.identity);
                    spawnedEnemyObj.GetComponent<WrongWay>().rightStart = true;
                    _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.WrongWay));
                }
                break;
            case EnemyTypes.Slick:
                spawnedEnemyObj = Instantiate(enemyTypes[3], _newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        StartCoroutine(SpawnEnemies());
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
